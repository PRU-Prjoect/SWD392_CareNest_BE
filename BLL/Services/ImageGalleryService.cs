using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageGalleryService : IImageGalleryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ImageGalleryService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<ImageGalleryResponse>> GetAllAsync(string? ownerId = null, string? nameFilter = null)
        {
            var images = await _unitOfWork._imageGalleryRepo.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(ownerId))
                images = images.Where(i => i.owner_id == ownerId).ToList();

            if (!string.IsNullOrWhiteSpace(nameFilter))
                images = images.Where(i => i.name != null && i.name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            return _mapper.Map<List<ImageGalleryResponse>>(images);
        }

        public async Task<ImageGalleryResponse?> GetByIdAsync(Guid id)
        {
            var image = await _unitOfWork._imageGalleryRepo.GetByIdAsync(id);
            return image == null ? null : _mapper.Map<ImageGalleryResponse>(image);
        }

        public async Task<ImageGalleryResponse> CreateAsync(ImageGalleryRequest dto)
        {
            var entity = _mapper.Map<ImageGallery>(dto);
            entity.id = Guid.NewGuid(); // Assign new ID

            if (dto.img != null)
            {
                var upload = await _cloudinaryService.UploadImage(dto.img);
                entity.img_url = upload.url;
            }

            await _unitOfWork._imageGalleryRepo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<ImageGalleryResponse>(entity);
        }

        public async Task<ImageGalleryResponse?> UpdateAsync(ImageGalleryRequest dto)
        {
            var entity = await _unitOfWork._imageGalleryRepo.GetByIdAsync(dto.id);
            if (entity == null) return null;

            entity.name = dto.name;
            entity.owner_id = dto.owner_id;

            if (dto.img == null)
            {
                if (!string.IsNullOrEmpty(entity.img_url))
                {
                    await _cloudinaryService.DeleteImage(entity.img_url);
                    entity.img_url = null;
                }
            }
            else if (string.IsNullOrEmpty(entity.img_url))
            {
                var uploaded = await _cloudinaryService.UploadImage(dto.img);
                entity.img_url = uploaded.url;
            }
            else
            {
                var updated = await _cloudinaryService.UpdateImage(dto.img, entity.img_url);
                entity.img_url = updated.url;
            }

            await _unitOfWork._imageGalleryRepo.UpdateAsync(entity);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<ImageGalleryResponse>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork._imageGalleryRepo.GetByIdAsync(id);
            if (entity == null) return false;

            if (!string.IsNullOrEmpty(entity.img_url))
            {
                await _cloudinaryService.DeleteImage(entity.img_url);
            }

            await _unitOfWork._imageGalleryRepo.UpdateAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
