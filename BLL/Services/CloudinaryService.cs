using BLL.Interfaces;
using BOL.Config;
using BOL.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryConfig _cloudinaryconfig;
        public CloudinaryService(IOptions<CloudinaryConfig> cloudinaryConfig)
        {
            var account = new CloudinaryDotNet.Account(cloudinaryConfig.Value.CloudName, cloudinaryConfig.Value.ApiKey, cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinaryconfig = cloudinaryConfig.Value;
        }

        public async Task<bool> DeleteImage(string publicId)
        {
            var deleteResult = await _cloudinary.DestroyAsync(new DeletionParams(publicId));
            if (deleteResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }

        public async Task<CloudinaryDTO> UpdateImage(IFormFile file, string publicId)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = publicId,
                Folder = _cloudinaryconfig.Folder,
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var result = new CloudinaryDTO();
            result.url = uploadResult.Url.ToString();
            result.publicId = uploadResult.PublicId;
            return result;
        }

        public async Task<CloudinaryDTO> UploadImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = _cloudinaryconfig.Folder,
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var result = new CloudinaryDTO();
            result.url = uploadResult.Url.ToString();
            result.publicId = uploadResult.PublicId;
            return result;
        }
    }
}
