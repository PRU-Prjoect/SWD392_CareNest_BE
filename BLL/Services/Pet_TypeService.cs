using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<List<Pet_TypeDTO>> GetAllAsync()
        {
            var petTypes = await _unitOfWork._pet_TypeRepo.GetAllAsync(); // Trả về List<Pet_Type>
            return _mapper.Map<List<Pet_TypeDTO>>(petTypes);
        }
    }
}
