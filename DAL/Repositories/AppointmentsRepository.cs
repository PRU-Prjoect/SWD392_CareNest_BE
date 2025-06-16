using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class AppointmentsRepository : GenericRepository<Appointments>, IAppointmentsRepository
    {
        private ApplicationDbContext _context;
        public AppointmentsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
