using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAllAsync(
            string name = null,
            bool? isActive = null,
            int? estimatedTime = null,
            Guid? serviceTypeId = null,
            Guid? shopId = null,
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
