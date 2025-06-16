using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IRoom_BookingService
    {
        Task<List<Room_BookingDTO>> GetAllAsync(
                Guid? roomDetailId = null,
                Guid? customerId = null,
                DateTime? checkInDate = null,
                DateTime? checkOutDate = null,
                bool? status = null);                                       // Get all room bookings with optional filtering by name
        Task<Room_BookingDTO> GetByIdAsync(Guid id);                       // Get a room booking by ID
        Task<bool> CreateAsync(Room_BookingDTO roomBookingDTO);            // Create a new room booking
        Task<bool> UpdateAsync(Room_BookingDTO roomBookingDTO);            // Update an existing room booking
        Task<bool> DeleteAsync(Guid id);                                    // Delete a room booking by ID
    }
}
