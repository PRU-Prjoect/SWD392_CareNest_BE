using DAL.Models;

namespace DAL.Interfaces
{
    public interface IShopRepository : IGenericRepository<Shop>
    {
        public Task<Shop> GetShopByIdAsync(Guid id);

        public Task<IEnumerable<Shop>> GetAllShopsAsync();
    }
}
