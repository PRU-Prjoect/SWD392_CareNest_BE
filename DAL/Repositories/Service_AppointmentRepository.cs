using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
