﻿using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RoomDTO>> GetAllAsync(Guid? hotelId = null, bool? isAvailable = null, int? roomType = null)
        {
            var rooms = await _unitOfWork._roomRepo.GetAllAsync();

            // Lọc theo hotelId nếu có
            if (hotelId.HasValue)
            {
                rooms = rooms.Where(r => r.hotel_id == hotelId.Value).ToList();
            }

            // Lọc theo isAvailable nếu có
            if (isAvailable.HasValue)
            {
                rooms = rooms.Where(r => r.is_available == isAvailable.Value).ToList();
            }

            // Lọc theo roomType nếu có
            if (roomType.HasValue)
            {
                rooms = rooms.Where(r => r.room_type == roomType.Value).ToList();
            }

            return _mapper.Map<List<RoomDTO>>(rooms);
        }

        public async Task<RoomDTO> GetByIdAsync(Guid id)
        {
            var room = await _unitOfWork._roomRepo.GetByIdAsync(id);
            return _mapper.Map<RoomDTO>(room);
        }

        public async Task<bool> CreateAsync(RoomDTO roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            room.id = Guid.NewGuid(); // Ensure a new ID is generated
            await _unitOfWork._roomRepo.AddAsync(room);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RoomDTO roomDto)
        {
            var check = await _unitOfWork._roomRepo.GetByIdAsync(roomDto.id)
                ?? throw new Exception();
           check.hotel_id= roomDto.hotel_id;
           check.is_available = roomDto.is_available;
           check.updated_at = DateTime.UtcNow;
           check.room_type = roomDto.room_type;
           check.room_number = roomDto.room_number;
            check.max_capacity = roomDto.max_capacity;
            check.daily_price = roomDto.daily_price;
            check.amendities = roomDto.amendities;

            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var room = await _unitOfWork._roomRepo.GetByIdAsync(id);
            if (room == null) return false;
            await _unitOfWork._roomRepo.RemoveAsync(room);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
