using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelDTO>> GetAllAsync(Guid? shopId = null, bool? isActive = null, string? nameFilter = null);
        Task<HotelDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(HotelDTO hotelDto);
        Task<bool> UpdateAsync(HotelDTO hotelDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
