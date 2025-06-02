using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStaffService
    {
        Task<List<StaffDTO>> GetAllAsync(); 
        Task<StaffDTO> GetByIdAsync(Guid staffId);
        Task<bool> CreateAsync(Guid accountId, Guid shopId, StaffDTO staffDto);
        Task<bool> UpdateAsync(StaffDTO staffDto); 
        Task<bool> DeleteAsync(Guid staffId);
        Task<bool> CancelStaffAsync(Guid staffId);
    }
}
