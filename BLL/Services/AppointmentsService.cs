using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
                FinishPercent = (FinishTotal*100)/Total,
                CancelPercent = (CancelTotal*100)/Total,
                InProgressPercent = (InProgressToTal*100)/Total,
                NoProgressPercent = (NoProgressTotal*100)/Total,

            };
            return appointmentReport;
        }
    }
}
