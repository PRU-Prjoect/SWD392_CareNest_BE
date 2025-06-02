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
        private readonly IPet_TypeRepository _pet_TypeRepository;

        public UnitOfWork(ApplicationDbContext Context,
            IAccountRepository AccountRepository,
            IPet_TypeRepository Pet_TypeRepository)
        {
            _context = Context;
            _accountRepository = AccountRepository;
            _pet_TypeRepository = Pet_TypeRepository;
        }

        public IAccountRepository _accountRepo => _accountRepository;
        public IPet_TypeRepository _pet_TypeRepo => _pet_TypeRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
