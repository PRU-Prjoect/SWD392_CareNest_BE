using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAllAsync(
            string? name = null,
            string? gender = null,
            string? email = null);
        Task<CustomerDTO> GetByIdAsync(Guid account_id);
        Task<bool> CreateAsync(Guid account_id, CustomerDTO customerDto);
        Task<bool> UpdateAsync(CustomerDTO customerDto);
    }
}
