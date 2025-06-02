using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISub_AddressService
    {
        Task<List<Sub_AddressDTO>> GetAllAsync(); 
        Task<Sub_AddressDTO> GetByIdAsync(Guid id);          
        Task<bool> CreateAsync(Sub_AddressDTO subAddressDto); 
        Task<bool> UpdateAsync(Sub_AddressDTO subAddressDto);             
        Task<bool> DeleteAsync(Guid id);
    }
}
