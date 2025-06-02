using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        private readonly ApplicationDbContext _context;
        public StaffRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
