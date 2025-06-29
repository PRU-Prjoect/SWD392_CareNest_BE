﻿using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IService_AppointmentService
    {
        Task<List<Service_AppointmentDTO>> GetAllAsync(
            Guid? serviceId = null,
            Guid? appointmentId = null,
            DateTime? startDate = null);
        Task<Service_AppointmentDTO> GetByIdAsync(Guid serviceId);
        Task<bool> CreateAsync(Service_AppointmentDTO serviceAppointmentDto);
        Task<bool> UpdateAsync(Service_AppointmentDTO serviceAppointmentDto);
        Task<bool> DeleteAsync(Guid serviceId);
    }
}
