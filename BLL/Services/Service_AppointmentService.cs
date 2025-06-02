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
    public class Service_AppointmentService : IService_AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Service_AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Service_AppointmentDTO>> GetAllAsync()
        {
            var data = await _unitOfWork._service_AppointmentRepo.GetAllAsync();
            return _mapper.Map<List<Service_AppointmentDTO>>(data);
        }

        public async Task<Service_AppointmentDTO> GetByIdAsync(Guid serviceId)
        {
            var data = await _unitOfWork._service_AppointmentRepo.GetByIdAsync(serviceId);
            return _mapper.Map<Service_AppointmentDTO>(data);
        }

        public async Task<bool> CreateAsync(Service_AppointmentDTO dto)
        {
            var entity = _mapper.Map<Service_Appointment>(dto);
            await _unitOfWork._service_AppointmentRepo.AddAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Service_AppointmentDTO dto)
        {
            var entity = _mapper.Map<Service_Appointment>(dto);
            await _unitOfWork._service_AppointmentRepo.UpdateAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid serviceId)
        {
            var entity = await _unitOfWork._service_AppointmentRepo.GetByIdAsync(serviceId);
            if (entity == null) return false;
            await _unitOfWork._service_AppointmentRepo.RemoveAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
