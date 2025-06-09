using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IShopRepository : IGenericRepository<Shop>
    {
        public Task<Shop> GetShopByIdAsync(Guid id);

        public Task<IEnumerable<Shop>> GetAllShopsAsync();
    }
}
