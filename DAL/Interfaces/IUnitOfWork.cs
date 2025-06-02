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
        public IPet_TypeRepository _pet_TypeRepo { get; }
        public IService_TypeRepository _service_TypeRepo { get; }
        public IShopRepository _shopRepo { get; }
        public IStaffRepository _staffRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
