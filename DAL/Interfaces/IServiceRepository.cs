using DAL.Models;

namespace DAL.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<Service> GetServiceByIdAsync(Guid id);
        Task<int> GetAppointmentCountByServiceIdAsync(Guid id);
    }
}
