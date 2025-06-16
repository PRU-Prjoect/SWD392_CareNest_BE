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

        public async Task<List<Service_TypeResponse>> GetAllAsync()
        {
            var serviceTypes = await _unitOfWork._service_TypeRepo.GetAllAsync();
            return _mapper.Map<List<Service_TypeResponse>>(serviceTypes);
        }

        public async Task<Service_TypeResponse> GetByIdAsync(Guid id)
        {
            var serviceType = await _unitOfWork._service_TypeRepo.GetByIdAsync(id);
            return _mapper.Map<Service_TypeResponse>(serviceType);
        }

        public async Task<Service_TypeResponse> UpdateAsync(Service_TypeRequest serviceTypeDTO)
        {
            serviceTypeDTO.is_public = false; // Default value
            var serviceType = _mapper.Map<Service_Type>(serviceTypeDTO);
            CloudinaryDTO cloudinaryDTO = new CloudinaryDTO();

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
            var entity = _mapper.Map<Service_Type>(serviceType);
            await _unitOfWork._service_TypeRepo.UpdateAsync(entity);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<Service_TypeResponse>(entity);
        }



    }
}
