using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<Service> GetServiceByIdAsync(Guid id);
        Task<int> GetAppointmentCountByServiceIdAsync(Guid id);
    }
}
