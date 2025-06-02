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

        public async Task<List<ServiceDTO>> GetAllAsync()
        {
            var services = await _unitOfWork._serviceRepo.GetAllAsync();
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var service = await _unitOfWork._serviceRepo.GetByIdAsync(id);
            if (service == null) return false;

            await _unitOfWork._serviceRepo.RemoveAsync(service);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
