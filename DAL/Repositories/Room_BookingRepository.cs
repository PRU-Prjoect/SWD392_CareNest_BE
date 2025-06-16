using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class Room_BookingRepository : GenericRepository<Room_Booking>, IRoom_BookingRepository
    {
        private readonly ApplicationDbContext _context;

        public Room_BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
