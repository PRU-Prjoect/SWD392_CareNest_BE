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
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AppointmentsDTO>> GetAllAsync()
        {
            var appointments = await _unitOfWork._appointmentsRepo.GetAllAsync();
            return _mapper.Map<List<AppointmentsDTO>>(appointments);
        }

        public async Task<AppointmentsDTO> GetByIdAsync(Guid id)
        {
            var appointment = await _unitOfWork._appointmentsRepo.GetByIdAsync(id);
            return _mapper.Map<AppointmentsDTO>(appointment);
        }

        public async Task<bool> CreateAsync(AppointmentsDTO appointmentDto)
        {
            var appointment = _mapper.Map<Appointments>(appointmentDto);
            await _unitOfWork._appointmentsRepo.AddAsync(appointment);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(AppointmentsDTO appointmentDto)
        {
            var appointment = _mapper.Map<Appointments>(appointmentDto);
            await _unitOfWork._appointmentsRepo.UpdateAsync(appointment);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var appointment = await _unitOfWork._appointmentsRepo.GetByIdAsync(id);
            if (appointment == null) return false;

            await _unitOfWork._appointmentsRepo.RemoveAsync(appointment);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
