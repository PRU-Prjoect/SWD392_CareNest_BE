using BOL.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICloudinaryService
    {
        Task<CloudinaryDTO> UploadImage (IFormFile file);
        Task<CloudinaryDTO> UpdateImage (IFormFile file, string publicId);
        Task<bool> DeleteImage (string publicId);
    }
}
