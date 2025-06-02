using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO> GetByIdAsync(Guid account_id);
        Task<bool> CreateAsync(Guid account_id, CustomerDTO customerDto);
        Task<bool> UpdateAsync(CustomerDTO customerDto);
    }
}
