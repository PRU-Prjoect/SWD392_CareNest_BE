using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IShopService
    {
        Task<List<ShopResponse>> GetAllAsync(string? name = null, bool? status = null);

        Task<ShopResponse> GetByIdAsync(Guid shopId);

        Task<bool> RegisterShopAsync(ShopRequest shopCreateDto);

        Task<bool> UpdateAsync(ShopRequest shopUpdateDto);

        Task<bool> DeleteAsync(Guid shopId);
    }
}
