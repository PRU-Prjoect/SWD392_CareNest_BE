using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public UnitOfWork(ApplicationDbContext Context,IAccountRepository AccountRepository)
        {
            _context = Context;
            _accountRepository = AccountRepository;
        }

        public IAccountRepository _accountRepo => _accountRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
