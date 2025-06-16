using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class Pet_TypeService : IPet_TypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Pet_TypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Pet_TypeDTO petTypeDTO)
        {
            var petType = _mapper.Map<Pet_Type>(petTypeDTO);
            await _unitOfWork._pet_TypeRepo.AddAsync(petType);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingPetType = await _unitOfWork._pet_TypeRepo.GetByIdAsync(id);
            if (existingPetType == null)
            {
                return false; // Pet type not found
            }
            await _unitOfWork._pet_TypeRepo.RemoveAsync(existingPetType);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<Pet_TypeDTO>> GetAllAsync(string? nameFilter = null)
        {
            var petTypes = await _unitOfWork._pet_TypeRepo.GetAllAsync();

            // Filtering by name if a filter is provided
            if (!string.IsNullOrEmpty(nameFilter))
            {
                petTypes = petTypes.Where(pt => pt.name != null && pt.name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return _mapper.Map<List<Pet_TypeDTO>>(petTypes);
        }

        public async Task<Pet_TypeDTO> GetByIdAsync(Guid id)
        {
            var petType = await _unitOfWork._pet_TypeRepo.GetByIdAsync(id);
            return _mapper.Map<Pet_TypeDTO>(petType);
        }

        public async Task<bool> UpdateAsync(Pet_TypeDTO petTypeDTO)
        {
            var existingPetType = await _unitOfWork._pet_TypeRepo.GetByIdAsync(petTypeDTO.id);
            if (existingPetType == null)
            {
                return false; // Pet type not found
            }

            _mapper.Map(petTypeDTO, existingPetType);
            await _unitOfWork._pet_TypeRepo.UpdateAsync(existingPetType);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
