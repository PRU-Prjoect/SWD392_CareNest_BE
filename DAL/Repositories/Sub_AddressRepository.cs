using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class Sub_AddressRepository : GenericRepository<Sub_Address>, ISub_AddressRepository
    {
        private readonly ApplicationDbContext _context;
        public Sub_AddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
