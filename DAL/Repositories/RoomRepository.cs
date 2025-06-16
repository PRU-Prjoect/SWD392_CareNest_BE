using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
