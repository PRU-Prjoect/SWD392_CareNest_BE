using BOL.DTOs;

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
