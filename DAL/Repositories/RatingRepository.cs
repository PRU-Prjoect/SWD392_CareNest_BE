using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private ApplicationDbContext _context;
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
