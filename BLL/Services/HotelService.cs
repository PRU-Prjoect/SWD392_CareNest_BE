using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HotelDTO>> GetAllAsync(Guid? shopId = null, bool? isActive = null, string? nameFilter = null)
        {
            var hotels = await _unitOfWork._hotelRepo.GetAllAsync();

            // Lọc theo shopId nếu có
            if (shopId.HasValue)
            {
                hotels = hotels.Where(h => h.shop_id == shopId.Value).ToList();
            }

            // Lọc theo isActive nếu có
            if (isActive.HasValue)
            {
                hotels = hotels.Where(h => h.is_active == isActive.Value).ToList();
            }

            // Lọc theo nameFilter nếu có
            if (!string.IsNullOrEmpty(nameFilter))
            {
                hotels = hotels.Where(h => h.name != null && h.name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return _mapper.Map<List<HotelDTO>>(hotels);
        }

        public async Task<HotelDTO> GetByIdAsync(Guid id)
        {
            var hotel = await _unitOfWork._hotelRepo.GetByIdAsync(id);
            return _mapper.Map<HotelDTO>(hotel);
        }

        public async Task<bool> CreateAsync(HotelDTO hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            hotel.id = Guid.NewGuid(); // Ensure a new ID is generated
            await _unitOfWork._hotelRepo.AddAsync(hotel);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HotelDTO hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _unitOfWork._hotelRepo.UpdateAsync(hotel);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var hotel = await _unitOfWork._hotelRepo.GetByIdAsync(id);
            if (hotel == null) return false;

            await _unitOfWork._hotelRepo.UpdateAsync(hotel);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
