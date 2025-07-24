using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class Room_BookingService : IRoom_BookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Room_BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Room_BookingDTO>> GetAllAsync(
            Guid? roomDetailId = null,
            Guid? customerId = null,
            DateTime? checkInDate = null,
            DateTime? checkOutDate = null,
            int? status = null)
        {
            var bookings = await _unitOfWork._room_BookingRepo.GetAllAsync();

            if (roomDetailId.HasValue)
                bookings = bookings.Where(b => b.room_detail_id == roomDetailId).ToList();

            if (customerId.HasValue)
                bookings = bookings.Where(b => b.customer_id == customerId).ToList();

            if (checkInDate.HasValue)
                bookings = bookings.Where(b => b.check_in_date.Date == checkInDate.Value.Date).ToList();

            if (checkOutDate.HasValue)
                bookings = bookings.Where(b => b.check_out_date.Date == checkOutDate.Value.Date).ToList();

            if (status.HasValue)
                bookings = bookings.Where(b => b.status == status).ToList();

            return _mapper.Map<List<Room_BookingDTO>>(bookings);
        }

        public async Task<Room_BookingDTO> GetByIdAsync(Guid id)
        {
            var booking = await _unitOfWork._room_BookingRepo.GetByIdAsync(id);
            return _mapper.Map<Room_BookingDTO>(booking);
        }

        public async Task<bool> CreateAsync(Room_BookingDTO dto)
        {
            var booking = _mapper.Map<Room_Booking>(dto);
            booking.id = Guid.NewGuid(); // Ensure a new ID is generated
            await _unitOfWork._room_BookingRepo.AddAsync(booking);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Room_BookingDTO dto)
        {
            var existing = await _unitOfWork._room_BookingRepo.GetByIdAsync(dto.id);
            if (existing == null) return false;

            _mapper.Map(dto, existing); // map changes
            existing.check_out_date = dto.check_out_date ; // handle nullable check_out_date
            existing.updated_at = DateTime.UtcNow; // update timestamp
            existing.check_in_date = dto.check_in_date;
            existing.total_night =(int) (dto.check_out_date - dto.check_in_date).TotalDays;
            existing.status = dto.status;
            existing.total_amount = dto.total_amount;
            existing.customer_id = dto.customer_id;
            existing.feeding_schedule = dto.feeding_schedule;
            existing.feeding_schedule = dto.feeding_schedule;

            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var booking = await _unitOfWork._room_BookingRepo.GetByIdAsync(id);
            if (booking == null) return false;

            await _unitOfWork._room_BookingRepo.RemoveAsync(booking);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
