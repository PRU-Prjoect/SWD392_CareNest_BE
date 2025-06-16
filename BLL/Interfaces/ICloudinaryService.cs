using BOL.DTOs;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICloudinaryService
    {
        Task<CloudinaryDTO> UploadImage(IFormFile file);
        Task<CloudinaryDTO> UpdateImage(IFormFile file, string publicId);
        Task<bool> DeleteImage(string publicId);
    }
}
