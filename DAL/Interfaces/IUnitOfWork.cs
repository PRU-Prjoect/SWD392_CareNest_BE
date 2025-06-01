using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepository _accountRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
