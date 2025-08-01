﻿using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IPet_Service_RoomService
    {
        Task<List<Pet_Service_RoomResponse>> GetAllAsync(Guid? ownerId = null, Guid? petTypeId = null, bool? isService = null);
        Task<Pet_Service_RoomResponse> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Pet_Service_RoomRequest petServiceRoomDto);
        Task<bool> UpdateAsync(Pet_Service_RoomRequest petServiceRoomDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
