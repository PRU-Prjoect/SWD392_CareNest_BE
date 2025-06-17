using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class Service_TypeService : IService_TypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public Service_TypeService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Service_TypeResponse> CreateAsync(Service_TypeRequest serviceTypeDTO)
        {
            
            serviceTypeDTO.is_public = false;
            var serviceType = _mapper.Map<Service_Type>(serviceTypeDTO);

            if (serviceTypeDTO.img != null)
            {
                var uploadResult = await _cloudinaryService.UploadImage(serviceTypeDTO.img);
                serviceType.img_url = uploadResult.url;
            }

            var entity = _mapper.Map<Service_Type>(serviceType);
            entity.id = Guid.NewGuid(); 
            await _unitOfWork._service_TypeRepo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<Service_TypeResponse>(entity);
        }




        //*****
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingServiceType = await _unitOfWork._service_TypeRepo.GetByIdAsync(id);
            await _unitOfWork._service_TypeRepo.RemoveAsync(existingServiceType);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<Service_TypeResponse>> GetAllAsync(string? name, bool? is_public)
        {
            var serviceTypes = await _unitOfWork._service_TypeRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
            {
                serviceTypes = serviceTypes.Where(x => x.name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Áp dụng filter theo is_public nếu có
            if (is_public.HasValue)
            {
                serviceTypes = serviceTypes.Where(x => x.is_public == is_public.Value).ToList();
            }

            List<Service_TypeResponse> result = _mapper.Map<List<Service_TypeResponse>>(serviceTypes);

            return result;
        }


        public async Task<Service_TypeResponse> GetByIdAsync(Guid id)
        {
            var serviceType = await _unitOfWork._service_TypeRepo.GetByIdAsync(id);
            return _mapper.Map<Service_TypeResponse>(serviceType);
        }

        public async Task<Service_TypeResponse> UpdateAsync(string id, Service_TypeRequest serviceTypeDTO)
        {
            Guid checkId = Guid.Parse(id);
            var serviceType = await _unitOfWork._service_TypeRepo.GetByIdAsync(checkId) ??
                throw new Exception("Service Type not found");

            CloudinaryDTO cloudinaryDTO = new CloudinaryDTO();

            // Xử lý image logic như cũ
            if (serviceTypeDTO.img == null)
            {
                if (serviceType.img_url != null)
                {
                    var result = await _cloudinaryService.DeleteImage(serviceType.img_url);
                }
                serviceType.img_url = null;
            }
            else if (serviceType.img_url == null)
            {
                cloudinaryDTO = await _cloudinaryService.UploadImage(serviceTypeDTO.img);
                serviceType.img_url = cloudinaryDTO.url;
            }
            else
            {
                cloudinaryDTO = await _cloudinaryService.UpdateImage(serviceTypeDTO.img, serviceType.img_url);
                serviceType.img_url = cloudinaryDTO.url;
            }

            serviceType.name = serviceTypeDTO.name;
            serviceType.description = serviceTypeDTO.description;
            serviceType.is_public = serviceTypeDTO.is_public;

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<Service_TypeResponse>(serviceType);
        }




    }
}
