using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAppointmentsService
    {
        Task<List<AppointmentsDTO>> GetAllAsync();
        Task<AppointmentsDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(AppointmentsDTO appointmentDto);
        Task<bool> UpdateAsync(AppointmentsDTO appointmentDto);
        Task<bool> DeleteAsync(Guid id);
        Task<AppointmentReportResponse> GetAppointmentreport();
    }
}
