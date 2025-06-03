using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Staff> GetStaffByIdAsync(Guid id)
        {
            return await _context.Staff.Include(a => a.account).FirstAsync(a => a.account_id == id);

        }
    }
}
