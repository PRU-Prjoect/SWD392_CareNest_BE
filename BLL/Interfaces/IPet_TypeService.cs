using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPet_TypeService
    {
        Task<List<Pet_TypeDTO>> GetAllAsync(string? nameFilter = null);
        Task<Pet_TypeDTO> GetByIdAsync(Guid id);           // Get a pet type by ID
        Task<bool> CreateAsync(Pet_TypeDTO petTypeDTO);    // Create a new pet type
        Task<bool> UpdateAsync(Pet_TypeDTO petTypeDTO);    // Update an existing pet type
        Task<bool> DeleteAsync(Guid id);                    // Delete a pet type by ID
    }
}
