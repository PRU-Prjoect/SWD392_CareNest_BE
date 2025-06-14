using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        public Task<Staff> GetStaffByIdAsync(Guid id);
        public Task<List<Staff>> GetAllStaff();

    }
}
