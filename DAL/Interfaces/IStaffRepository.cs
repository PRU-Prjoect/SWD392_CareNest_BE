using DAL.Models;

namespace DAL.Interfaces
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        public Task<Staff> GetStaffByIdAsync(Guid id);
        public Task<List<Staff>> GetAllStaff();

    }
}
