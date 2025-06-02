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
        Task<List<ShopDTO>> GetAllAsync();

        Task<ShopDTO> GetByIdAsync(Guid shopId);

        Task<bool> RegisterShopAsync(Guid accountId, ShopDTO shopCreateDto);

        Task<bool> UpdateAsync(ShopDTO shopUpdateDto);

        Task<bool> DeleteAsync(Guid shopId);
    }
}
