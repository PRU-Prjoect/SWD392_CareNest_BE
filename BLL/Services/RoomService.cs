using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<RoomDTO>> GetAllAsync()
        {
            var rooms = await _unitOfWork._roomRepo.GetAllAsync();
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
            await _unitOfWork._roomRepo.AddAsync(room);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RoomDTO roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _unitOfWork._roomRepo.UpdateAsync(room);
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
