using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ServiceDTO>> GetAllAsync(
            string name = null,
            bool? isActive = null,
            int? estimatedTime = null,
            Guid? serviceTypeId = null,
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

            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<ServiceDTO> GetByIdAsync(Guid id)
        {
            var service = await _unitOfWork._serviceRepo.GetByIdAsync(id);
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<bool> CreateAsync(ServiceDTO serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            await _unitOfWork._serviceRepo.AddAsync(service);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ServiceDTO serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            await _unitOfWork._serviceRepo.UpdateAsync(service);
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

            // Cập nhật thông tin dịch vụ trong cơ sở dữ liệu
            await _unitOfWork._serviceRepo.UpdateAsync(service);

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
            service.Star = newRating; // Tổng số sao hiện tại

            // Cập nhật dịch vụ
            await _unitOfWork._serviceRepo.UpdateAsync(service);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAppointmentCount(Guid serviceId)
        {
            var service = await _unitOfWork._serviceRepo.GetServiceByIdAsync(serviceId);

            if (service == null) return false;

            // Gọi API để đếm số lượng cuộc hẹn
            int appointmentCount = await _unitOfWork._serviceRepo.GetAppointmentCountByServiceIdAsync(serviceId);

            // Cập nhật số lượng cuộc hẹn
            service.purchases = appointmentCount;

            // Cập nhật dịch vụ
            await _unitOfWork._serviceRepo.UpdateAsync(service);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
