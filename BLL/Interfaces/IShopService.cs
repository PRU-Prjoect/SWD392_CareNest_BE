using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
