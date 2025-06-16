using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class Service_TypeRepository : GenericRepository<Service_Type>, IService_TypeRepository
    {
        private readonly ApplicationDbContext _context;

        public Service_TypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
