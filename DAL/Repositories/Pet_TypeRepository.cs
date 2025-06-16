using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class Pet_TypeRepository : GenericRepository<Pet_Type>, IPet_TypeRepository
    {
        private ApplicationDbContext _context;
        public Pet_TypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
