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
        Task<List<Sub_AddressRequest>> GetAllAsync(); 
        Task<Sub_AddressRequest> GetByIdAsync(Guid id);          
        Task<bool> CreateAsync(Sub_AddressRequest subAddressDto); 
        Task<bool> UpdateAsync(Sub_AddressRequest subAddressDto);             
        Task<bool> DeleteAsync(Guid id);
    }
}
