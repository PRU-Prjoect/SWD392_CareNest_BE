using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
