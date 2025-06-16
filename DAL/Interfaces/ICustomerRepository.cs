using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<List<Customer>> GetAllCustomer();
    }
}
