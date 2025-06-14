using BOL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAllAsync(
            string name = null,
            bool? isActive = null,
            int? estimatedTime = null,
            Guid? serviceTypeId = null,
            string sortBy = "createdAt");
        Task<ServiceDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(ServiceDTO serviceDto);
        Task<bool> UpdateAsync(ServiceDTO serviceDto);
        Task<bool> CancelService(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateStarAverage(Guid serviceId, int newRating);
        Task<bool> UpdateAppointmentCount(Guid serviceId);

    }
}
