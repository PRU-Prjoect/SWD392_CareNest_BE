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
        private readonly IService_TypeRepository _service_TypeRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISub_AddressRepository _sub_AddressRepository;

        public UnitOfWork(ApplicationDbContext Context,
            IAccountRepository AccountRepository,
            IPet_TypeRepository Pet_TypeRepository,
            IService_TypeRepository service_TypeRepository,
            IShopRepository shopRepository,
            IStaffRepository staffRepository,
            ICustomerRepository customerRepository,
            ISub_AddressRepository sub_AddressRepository)
        {
            _context = Context;
            _accountRepository = AccountRepository;
            _pet_TypeRepository = Pet_TypeRepository;
            _service_TypeRepository = service_TypeRepository;
            _shopRepository = shopRepository;
            _staffRepository = staffRepository;
            _customerRepository = customerRepository;
            _sub_AddressRepository = sub_AddressRepository;
        }

        public IAccountRepository _accountRepo => _accountRepository;
        public IPet_TypeRepository _pet_TypeRepo => _pet_TypeRepository;
        public IService_TypeRepository _service_TypeRepo => _service_TypeRepository;
        public IShopRepository _shopRepo => _shopRepository;
        public IStaffRepository _staffRepo => _staffRepository;
        public ICustomerRepository _customerRepo => _customerRepository;
        public ISub_AddressRepository _sub_AddressRepo => _sub_AddressRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
