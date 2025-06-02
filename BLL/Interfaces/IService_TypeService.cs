using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService_TypeService
    {
        Task<List<Service_TypeDTO>> GetAllAsync();           // Get all service types
        Task<Service_TypeDTO> GetByIdAsync(Guid id);         // Get a service type by ID
        Task<bool> CreateAsync(Service_TypeDTO serviceTypeDTO); // Create a new service type
        Task<bool> UpdateAsync(Service_TypeDTO serviceTypeDTO); // Update an existing service type
        Task<bool> DeleteAsync(Guid id);
    }
}
