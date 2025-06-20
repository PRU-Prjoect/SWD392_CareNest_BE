using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelResponse>> GetAllAsync(Guid? shopId = null, bool? isActive = null, string? nameFilter = null);
        Task<HotelResponse> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(HotelDTO hotelDto);
        Task<bool> UpdateAsync(HotelDTO hotelDto, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<HotelReportResponse> GetHotelReport(Guid shopId);
    }
}
