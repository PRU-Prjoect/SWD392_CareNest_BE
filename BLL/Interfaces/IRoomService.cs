using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDTO>> GetAllAsync(Guid? hotelId = null, bool? isAvailable = null, int? roomType = null);
        Task<RoomDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(RoomDTO roomDto);
        Task<bool> UpdateAsync(RoomDTO roomDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
