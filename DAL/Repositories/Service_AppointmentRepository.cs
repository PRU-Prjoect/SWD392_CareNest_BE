using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class Service_AppointmentRepository : GenericRepository<Service_Appointment>, IService_AppointmentRepository
    {
        private ApplicationDbContext _context;
        public Service_AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
