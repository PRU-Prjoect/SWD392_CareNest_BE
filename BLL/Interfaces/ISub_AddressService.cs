using BOL;
using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface ISub_AddressService
    {
        Task<List<Sub_AddressResponse>> GetAllAsync(Guid? shopId = null, string? addressName = null, bool? isDefault = null);
        Task<Sub_AddressResponse> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Sub_AddressDTO subAddressDto);
        Task<bool> UpdateAsync(Sub_AddressDTO subAddressDto, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
