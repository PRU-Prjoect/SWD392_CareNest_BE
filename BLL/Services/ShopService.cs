using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using CloudinaryDotNet;
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

            // Lấy thông tin tài khoản của shop từ cơ sở dữ liệu
            var shopAccount = await _unitOfWork._accountRepo.GetByIdAsync(shopCreateDto.account_id);

            if (shopAccount != null)
            {
                // Cập nhật vai trò của tài khoản thành "shop"
                shopAccount.role = Role.Shop;

                // Cập nhật tài khoản
                await _unitOfWork._accountRepo.UpdateAsync(shopAccount);

                // Thêm cửa hàng vào cơ sở dữ liệu
                await _unitOfWork._shopRepo.AddAsync(shop);

                // Lưu tất cả thay đổi
                return await _unitOfWork.SaveChangeAsync() > 0;
            }

            return false; // Nếu không tìm thấy tài khoản
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
