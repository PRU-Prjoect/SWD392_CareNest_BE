using AutoMapper;
using BLL.Interfaces;
using BOL;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

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

        public async Task<List<Sub_AddressResponse>> GetAllAsync(Guid? shopId = null, string? addressName = null, bool? isDefault = null)
        {
            var subAddresses = await _unitOfWork._sub_AddressRepo.GetAllAsync();

            // Lọc theo shopId nếu có
            if (shopId.HasValue)
            {
                subAddresses = subAddresses.Where(sa => sa.shop_id == shopId.Value).ToList();
            }   

            // Lọc theo addressName nếu có
            if (!string.IsNullOrEmpty(addressName))
            {
                subAddresses = subAddresses.Where(sa => sa.address_name != null && sa.address_name.Contains(addressName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Lọc theo isDefault nếu có
            if (isDefault.HasValue)
            {
                subAddresses = subAddresses.Where(sa => sa.is_default == isDefault.Value).ToList();
            }

            return _mapper.Map<List<Sub_AddressResponse>>(subAddresses);

        }

        public async Task<Sub_AddressResponse> GetByIdAsync(Guid id)
        {
            var subAddress = await _unitOfWork._sub_AddressRepo.GetByIdAsync(id);
            return _mapper.Map<Sub_AddressResponse>(subAddress);
        }

        public async Task<bool> CreateAsync(Sub_AddressDTO subAddressDto)
        {
            var subAddress = _mapper.Map<Sub_Address>(subAddressDto);
            subAddress.id = Guid.NewGuid(); // Ensure a new ID is generated
            await _unitOfWork._sub_AddressRepo.AddAsync(subAddress);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Sub_AddressDTO subAddressDto, Guid id)
        {
            var check = await _unitOfWork._sub_AddressRepo.GetByIdAsync(id)
                ?? throw new Exception();

            check.phone = subAddressDto.phone;
            check.name = subAddressDto.name;
            check.is_default = subAddressDto.is_default;
            check.address_name = subAddressDto.address_name;
            check.updated_at = DateTime.UtcNow;
    
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
