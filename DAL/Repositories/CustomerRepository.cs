using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _context.Customer
                .Include(s => s.account).ToListAsync();

        }
    }
}
