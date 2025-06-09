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
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShopService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get all shops
        public async Task<List<ShopResponse>> GetAllAsync(string? name = null, bool? status = null)
        {
            var shops = await _unitOfWork._shopRepo.GetAllShopsAsync();

            // Apply filtering by Name
            if (!string.IsNullOrEmpty(name))
            {
                shops = shops.Where(shop => shop.name != null && shop.name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply filtering by Status
            if (status.HasValue)
            {
                shops = shops.Where(shop => shop.status == status.Value).ToList();
            }

            return _mapper.Map<List<ShopResponse>>(shops);
        }

        // Get shop by id
        public async Task<ShopResponse> GetByIdAsync(Guid shopId)
        {
            var shop = await _unitOfWork._shopRepo.GetShopByIdAsync(shopId);
            return _mapper.Map<ShopResponse>(shop);
        }

        // Register shop (with accountId as FK)
        public async Task<bool> RegisterShopAsync(ShopRequest shopCreateDto)
        {
            var shop = _mapper.Map<Shop>(shopCreateDto);

            await _unitOfWork._shopRepo.AddAsync(shop);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        // Update shop
        public async Task<bool> UpdateAsync(ShopRequest shopUpdateDto)
        {

            var shop = _mapper.Map<Shop>(shopUpdateDto);

            await _unitOfWork._shopRepo.UpdateAsync(shop);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        // Delete shop
        public async Task<bool> DeleteAsync(Guid shopId)
        {
            var shop = await _unitOfWork._shopRepo.GetByIdAsync(shopId);
            if (shop == null) return false;

            await _unitOfWork._shopRepo.RemoveAsync(shop);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
