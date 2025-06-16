using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await _context.Account.FirstOrDefaultAsync(a => a.email == email);
        }

        public async Task<Account?> GetByUsernameAsync(string username)
        {
            return await _context.Account.FirstOrDefaultAsync(a => a.username == username);
        }
    }
}
