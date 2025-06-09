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
    public class Service_TypeService : IService_TypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Service_TypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Service_TypeDTO serviceTypeDTO)
        {
            serviceTypeDTO.is_public = false; // Default value
            var serviceType = _mapper.Map<Service_Type>(serviceTypeDTO);
            await _unitOfWork._service_TypeRepo.AddAsync(serviceType);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }


        //*****
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingServiceType = await _unitOfWork._service_TypeRepo.GetByIdAsync(id);
            await _unitOfWork._service_TypeRepo.RemoveAsync(existingServiceType);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<Service_TypeDTO>> GetAllAsync()
        {
            var serviceTypes = await _unitOfWork._service_TypeRepo.GetAllAsync();
            return _mapper.Map<List<Service_TypeDTO>>(serviceTypes);
        }

        public async  Task<Service_TypeDTO> GetByIdAsync(Guid id)
        {
            var serviceType = await _unitOfWork._service_TypeRepo.GetByIdAsync(id);
            return _mapper.Map<Service_TypeDTO>(serviceType);
        }

        public async Task<bool> UpdateAsync(Service_TypeDTO serviceTypeDTO)
        {
            serviceTypeDTO.is_public = false; // Default value
            var serviceType = _mapper.Map<Service_Type>(serviceTypeDTO);
            await _unitOfWork._service_TypeRepo.UpdateAsync(serviceType);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
