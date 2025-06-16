using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface ISub_AddressService
    {
        Task<List<Sub_AddressDTO>> GetAllAsync(Guid? shopId = null, string? addressName = null, bool? isDefault = null);
        Task<Sub_AddressDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Sub_AddressDTO subAddressDto);
        Task<bool> UpdateAsync(Sub_AddressDTO subAddressDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
