using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private ApplicationDbContext _context;
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
