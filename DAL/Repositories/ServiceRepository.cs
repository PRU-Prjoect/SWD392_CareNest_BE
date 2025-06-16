using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Service> GetServiceByIdAsync(Guid id)
        {
            return await _context.Service.Include(a => a.service_appointment).FirstAsync(a => a.id == id);

        }

        public async Task<int> GetAppointmentCountByServiceIdAsync(Guid id)
        {
            var service = await _context.Service
                .Include(a => a.service_appointment)
                .FirstAsync(a => a.id == id);

            // Ensure service_appointment is not null before calling Count()
            return service?.service_appointment?.Count() ?? 0;
        }
    }
}
