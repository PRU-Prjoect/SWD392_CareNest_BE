using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using CloudinaryDotNet;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<ServiceResponse>> GetAllAsync(
            string name = null,
            bool? isActive = null,
            int? estimatedTime = null,
            Guid? serviceTypeId = null,
            Guid? shopId = null,
            string sortBy = "createdAt") // Tham số sắp xếp
        {
            var services = await _unitOfWork._serviceRepo.GetAllAsync();

            // Lọc theo name nếu có
            if (!string.IsNullOrEmpty(name))
            {
                services = services.Where(s => s.name.Contains(name)).ToList();
            }

            // Lọc theo isActive nếu có
            if (isActive.HasValue)
            {
                services = services.Where(s => s.is_active == isActive.Value).ToList();
            }

            // Lọc theo estimatedTime nếu có
            if (estimatedTime.HasValue)
            {
                services = services.Where(s => s.duration_type == estimatedTime.Value).ToList();
            }

            // Lọc theo serviceTypeId nếu có
            if (serviceTypeId.HasValue)
            {
                services = services.Where(s => s.service_type_id == serviceTypeId.Value).ToList();
            }

            // Lọc theo shopId nếu có
            if (shopId.HasValue)
            {
                services = services.Where(s => s.shop_id == shopId.Value).ToList();
            }

            // Sắp xếp
            switch (sortBy)
            {
                case "createdAt":
                    services = services.OrderByDescending(s => s.created_at).ToList();
                    break;
                case "purchases":
                    services = services.OrderByDescending(s => s.purchases).ToList();
                    break;
                case "star":
                    services = services.OrderByDescending(s => s.Star).ToList();
                    break;
                case "priceDesc":
                    services = services.OrderByDescending(s => s.Price).ToList();
                    break;
                case "priceAsc":
                    services = services.OrderBy(s => s.Price).ToList();
                    break;
                default:
                    services = services.OrderByDescending(s => s.created_at).ToList(); // Sắp xếp mặc định
                    break;
            }
            List<ServiceResponse> serviceResponse = new List<ServiceResponse>();
            serviceResponse = _mapper.Map<List<ServiceResponse>>(services);

            return serviceResponse;
        }

        public async Task<ServiceDTO> GetByIdAsync(Guid id)
        {
            var service = await _unitOfWork._serviceRepo.GetByIdAsync(id);
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<bool> CreateAsync(ServiceDTO serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            if (serviceDto.img != null)
            {
                CloudinaryDTO cloudinaryDTO = await _cloudinaryService.UploadImage(serviceDto.img);
                service.img_url = cloudinaryDTO.url;
                service.img_url_id = cloudinaryDTO.publicId;
            }
            await _unitOfWork._serviceRepo.AddAsync(service);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ServiceDTO serviceDto)
        {
            var check = await _unitOfWork._serviceRepo.GetByIdAsync(serviceDto.id)
                ?? throw new Exception();

            check.limit_per_hour = serviceDto.limit_per_hour;
            check.name = serviceDto.name;
            check.description = serviceDto.description;
            check.updated_at = DateTime.UtcNow;
            check.purchases = serviceDto.purchases;
            check.Price = serviceDto.Price;
            check.is_active = serviceDto.is_active;
            check.service_type_id = serviceDto.service_type_id;
 // Xử lý cập nhật hình ảnh
    if (serviceDto.img != null)
    {
        // Nếu đã có ảnh cũ, xóa ảnh cũ trước
        if (!string.IsNullOrEmpty(service.img_url_id))
        {
            await _cloudinaryService.DeleteImage(service.img_url_id);
        }
        
        // Upload ảnh mới
        CloudinaryDTO cloudinaryDTO = await _cloudinaryService.UploadImage(serviceDto.img);
        service.img_url = cloudinaryDTO.url;
        service.img_url_id = cloudinaryDTO.publicId;
    }
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> CancelService(Guid id)
        {
            // Lấy thông tin dịch vụ theo ID
            var service = await _unitOfWork._serviceRepo.GetServiceByIdAsync(id);

            // Kiểm tra xem dịch vụ có tồn tại không
            if (service == null)
            {
                return false; // Dịch vụ không tồn tại
            }

            // Kiểm tra xem dịch vụ có đặt chỗ không
            if (service.service_appointment != null && service.service_appointment.Any())
            {
                return false; // Không thể hủy dịch vụ vì có đặt chỗ
            }

            // Đặt trạng thái không hoạt động cho dịch vụ
            service.is_active = false;
            // Lưu thay đổi vào cơ sở dữ liệu và trả về kết quả
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var service = await _unitOfWork._serviceRepo.GetByIdAsync(id);
            if (service == null) return false;
            if (service.is_active)
            {
                return false; // Không thể xóa dịch vụ vì nó vẫn đang hoạt động
            }

            await _unitOfWork._serviceRepo.RemoveAsync(service);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateStarAverage(Guid serviceId, int newRating)
        {
            var service = await _unitOfWork._serviceRepo.GetByIdAsync(serviceId);

            if (service == null) return false;

            // Cập nhật số sao trung bình
            service.Star = (newRating + service.Star) / 2;

            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAppointmentCount(Guid serviceId)
        {
            var service = await _unitOfWork._serviceRepo.GetServiceByIdAsync(serviceId);

            if (service == null) return false;

            // Gọi API để đếm số lượng cuộc hẹn
            int appointmentCount = await _unitOfWork._serviceRepo.GetAppointmentCountByServiceIdAsync(serviceId);

            // Cập nhật số lượng cuộc hẹn
            service.purchases = appointmentCount + 1;

            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
