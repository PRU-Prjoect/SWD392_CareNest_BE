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
        public ICustomerRepository _customerRepo { get; }
        public ISub_AddressRepository _sub_AddressRepo { get; }
        public IServiceRepository _serviceRepo { get; }
        public IAppointmentsRepository _appointmentsRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
