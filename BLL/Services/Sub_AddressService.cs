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
    public class Sub_AddressService : ISub_AddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Sub_AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Sub_AddressDTO>> GetAllAsync()
        {
            var subAddresses = await _unitOfWork._sub_AddressRepo.GetAllAsync();
            return _mapper.Map<List<Sub_AddressDTO>>(subAddresses);
        }

        public async Task<Sub_AddressDTO> GetByIdAsync(Guid id)
        {
            var subAddress = await _unitOfWork._sub_AddressRepo.GetByIdAsync(id);
            return _mapper.Map<Sub_AddressDTO>(subAddress);
        }

        public async Task<bool> CreateAsync(Sub_AddressDTO subAddressDto)
        {
            var subAddress = _mapper.Map<Sub_Address>(subAddressDto);

            await _unitOfWork._sub_AddressRepo.AddAsync(subAddress);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Sub_AddressDTO subAddressDto)
        {
            var subAddress = _mapper.Map<Sub_Address>(subAddressDto);

            await _unitOfWork._sub_AddressRepo.UpdateAsync(subAddress);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var subAddress = await _unitOfWork._sub_AddressRepo.GetByIdAsync(id);
            if (subAddress == null) return false;

            await _unitOfWork._sub_AddressRepo.RemoveAsync(subAddress);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

    }
}
