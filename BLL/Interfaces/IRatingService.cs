using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRatingService
    {
        Task<List<RatingDTO>> GetAllAsync();
        Task<RatingDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(RatingDTO ratingDto);
        Task<bool> UpdateAsync(RatingDTO ratingDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
