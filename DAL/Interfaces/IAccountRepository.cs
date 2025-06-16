using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<Account?> GetByUsernameAsync(string username);
        public Task<Account?> GetByEmailAsync(string email);
    }
}
