using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class Pet_Service_RoomService : IPet_Service_RoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Pet_Service_RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Pet_Service_RoomResponse>> GetAllAsync(Guid? ownerId = null, Guid? petTypeId = null, bool? isService = null)
        {
            var petServiceRooms = await _unitOfWork._pet_Service_RoomRepo.GetAllAsync();

            // Lọc theo ownerId nếu có
            if (ownerId.HasValue)
            {
                petServiceRooms = petServiceRooms.Where(psr => psr.owner_id == ownerId.Value).ToList();
            }

            // Lọc theo petTypeId nếu có
            if (petTypeId.HasValue)
            {
                petServiceRooms = petServiceRooms.Where(psr => psr.pet_type_id == petTypeId.Value).ToList();
            }

            // Lọc theo isService nếu có
            if (isService.HasValue)
            {
                petServiceRooms = petServiceRooms.Where(psr => psr.is_service == isService.Value).ToList();
            }

            return _mapper.Map<List<Pet_Service_RoomResponse>>(petServiceRooms);
        }

        public async Task<Pet_Service_RoomResponse> GetByIdAsync(Guid id)
        {
            var petServiceRoom = await _unitOfWork._pet_Service_RoomRepo.GetByIdAsync(id);
            return _mapper.Map<Pet_Service_RoomResponse>(petServiceRoom);
        }

        public async Task<bool> CreateAsync(Pet_Service_RoomRequest petServiceRoomDto)
        {
            {
                // Tạo một thực thể mới từ DTO
                var petServiceRoom = _mapper.Map<Pet_Service_Room>(petServiceRoomDto);

                // Xác định owner_id dựa trên is_service
                petServiceRoom.owner_id = petServiceRoomDto.is_service ? petServiceRoomDto.service_id : petServiceRoomDto.room_id;

                // Thêm vào cơ sở dữ liệu
                await _unitOfWork._pet_Service_RoomRepo.AddAsync(petServiceRoom);
                return await _unitOfWork.SaveChangeAsync() > 0;
            }
        }

        public async Task<bool> UpdateAsync(Pet_Service_RoomRequest petServiceRoomDto)
        {
            var petServiceRoom = await _unitOfWork._pet_Service_RoomRepo.GetByIdAsync(petServiceRoomDto.id);
            if (petServiceRoom == null) return false;

            petServiceRoom.pet_type_id = petServiceRoomDto.pet_type_id;
            petServiceRoom.is_service = petServiceRoomDto.is_service;
            petServiceRoom.service_id = petServiceRoomDto.service_id;
            petServiceRoom.room_id = petServiceRoomDto.room_id;
            petServiceRoom.owner_id = petServiceRoomDto.is_service ? petServiceRoomDto.service_id : petServiceRoomDto.room_id;

            await _unitOfWork._pet_Service_RoomRepo.UpdateAsync(petServiceRoom);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var petServiceRoom = await _unitOfWork._pet_Service_RoomRepo.GetByIdAsync(id);
            if (petServiceRoom == null) return false;

            await _unitOfWork._pet_Service_RoomRepo.RemoveAsync(petServiceRoom);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
