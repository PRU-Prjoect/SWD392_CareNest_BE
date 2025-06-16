using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IImageGalleryService 
    {
        Task<List<ImageGalleryResponse>> GetAllAsync(
            string? ownerId = null,
            string? nameFilter = null
        );

        Task<ImageGalleryResponse?> GetByIdAsync(Guid id);

        Task<ImageGalleryResponse> CreateAsync(ImageGalleryRequest dto);

        Task<ImageGalleryResponse?> UpdateAsync(ImageGalleryRequest dto);

        Task<bool> DeleteAsync(Guid id);
    }
}
