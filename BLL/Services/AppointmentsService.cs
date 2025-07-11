﻿using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL.Interfaces;
using DAL.Models;

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
        public async Task<List<AppointmentsDTO>> GetAllAsync(
            Guid? customerId = null,
            AppointmentStatus? status = null,
            DateTime? startTime = null)
        {
            // Lấy tất cả các cuộc hẹn
            var appointmentsQuery = await _unitOfWork._appointmentsRepo.GetAllAsync();

            // Lọc theo customerId nếu có
            if (customerId.HasValue)
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.customer_id == customerId.Value).ToList();
            }

            // Lọc theo status nếu có
            if (status.HasValue)
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.status == status.Value).ToList();
            }

            // Lọc theo startTime nếu có
            if (startTime.HasValue)
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.start_time.Date == startTime.Value.Date).ToList();
            }

            // Chuyển đổi sang danh sách DTO
            return _mapper.Map<List<AppointmentsDTO>>(appointmentsQuery);
        }

        public async Task<AppointmentsDTO> GetByIdAsync(Guid id)
        {
            var appointment = await _unitOfWork._appointmentsRepo.GetByIdAsync(id);
            return _mapper.Map<AppointmentsDTO>(appointment);
        }

        public async Task<AppointmentsDTO?> CreateAsync(AppointmentsDTO appointmentDto)
        {
            appointmentDto.id = Guid.NewGuid();
            var appointment = _mapper.Map<Appointments>(appointmentDto);

            await _unitOfWork._appointmentsRepo.AddAsync(appointment);
            var success = await _unitOfWork.SaveChangeAsync() > 0;

            if (!success) return null;

            return await GetByIdAsync(appointmentDto.id);
        }


        public async Task<bool> UpdateAsync(AppointmentsDTO appointmentDto)
        {
            var existingAppointment = await _unitOfWork._appointmentsRepo.GetByIdAsync(appointmentDto.id)
                ?? throw new Exception();
            existingAppointment.customer_id = appointmentDto.customer_id;
            existingAppointment.start_time = appointmentDto.start_time;
            existingAppointment.status = appointmentDto.status;
            existingAppointment.notes = appointmentDto.notes;
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var appointment = await _unitOfWork._appointmentsRepo.GetByIdAsync(id);
            if (appointment == null) return false;
            await _unitOfWork._appointmentsRepo.RemoveAsync(appointment);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<AppointmentReportResponse> GetAppointmentreport()
        {
            var appointments = await _unitOfWork._appointmentsRepo.GetAllAsync();
            int Total = appointments.Count();
            if (Total == 0)
            {
                return new AppointmentReportResponse();
            }

            int FinishTotal = 0;
            int CancelTotal = 0;
            int InProgressToTal = 0;
            int NoProgressTotal = 0;
            foreach (var appointment in appointments)
            {
                if (appointment.status == AppointmentStatus.Finish)
                {
                    FinishTotal++;
                }
                if (appointment.status == AppointmentStatus.Cancel)
                {
                    CancelTotal++;
                }
                if (appointment.status == AppointmentStatus.InProgress)
                {
                    InProgressToTal++;
                }
                if (appointment.status == AppointmentStatus.NoProgress)
                {
                    NoProgressTotal++;
                }
            }

            var appointmentReport = new AppointmentReportResponse
            {
                Total = Total,
                Finish = FinishTotal,
                Cancel = CancelTotal,
                InProgress = InProgressToTal,
                NoProgress = NoProgressTotal,
                FinishPercent = ((float)FinishTotal * 100f) / Total,
                CancelPercent = ((float)CancelTotal * 100f) / Total,
                InProgressPercent = ((float)InProgressToTal * 100f) / Total,
                NoProgressPercent = ((float)NoProgressTotal * 100f) / Total,

            };
            return appointmentReport;
        }
    }
}
