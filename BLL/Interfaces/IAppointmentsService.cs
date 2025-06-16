using BOL.DTOs;
using BOL.Enums;

namespace BLL.Interfaces
{
    public interface IAppointmentsService
    {
        Task<List<AppointmentsDTO>> GetAllAsync(
            Guid? customerId = null,
            AppointmentStatus? status = null,
            DateTime? startTime = null);
        Task<AppointmentsDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(AppointmentsDTO appointmentDto);
        Task<bool> UpdateAsync(AppointmentsDTO appointmentDto);
        Task<bool> DeleteAsync(Guid id);
        Task<AppointmentReportResponse> GetAppointmentreport();
    }
}
