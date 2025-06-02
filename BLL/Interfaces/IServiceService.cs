using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAllAsync();
        Task<ServiceDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(ServiceDTO serviceDto);
        Task<bool> UpdateAsync(ServiceDTO serviceDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
