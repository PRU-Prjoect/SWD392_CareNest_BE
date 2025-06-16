using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL.Interfaces;
using DAL.Models;

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
            // Lấy cửa hàng theo shopId
            var shop = await _unitOfWork._shopRepo.GetByIdAsync(shopId);
            if (shop == null) return false; // Kiểm tra null

            // Kiểm tra các cuộc hẹn có trạng thái 'InProgress'
            var services = shop.service ?? new List<Service>(); // Kiểm tra null
            var serviceAppointments = services.SelectMany(s => s.service_appointment ?? Enumerable.Empty<Service_Appointment>()).ToList();
            var appointments = serviceAppointments.Select(sa => sa.appointment ?? new Appointments()).ToList();

            if (appointments.Any(a => a.status == AppointmentStatus.InProgress))
            {
                return false; // Không thể xóa nếu có cuộc hẹn chưa hoàn thành
            }

            try
            {
                // Xóa nhân viên
                foreach (var staff in shop.staff ?? Enumerable.Empty<Staff>())
                {
                    await _unitOfWork._staffRepo.RemoveAsync(staff);
                }

                // Xóa địa chỉ phụ
                foreach (var subAddress in shop.sub_address ?? Enumerable.Empty<Sub_Address>())
                {
                    await _unitOfWork._sub_AddressRepo.RemoveAsync(subAddress);
                }

                // Xóa khách sạn và phòng
                foreach (var hotel in shop.hotel ?? Enumerable.Empty<Hotel>())
                {
                    foreach (var room in hotel.room ?? Enumerable.Empty<Room>())
                    {
                        await _unitOfWork._roomRepo.RemoveAsync(room);
                    }
                    await _unitOfWork._hotelRepo.RemoveAsync(hotel);
                }

                // Xóa dịch vụ và phòng dịch vụ
                foreach (var service in services)
                {
                    foreach (var petServiceRoom in service.room ?? Enumerable.Empty<Pet_Service_Room>())
                    {
                        await _unitOfWork._pet_Service_RoomRepo.RemoveAsync(petServiceRoom);
                    }
                    await _unitOfWork._serviceRepo.RemoveAsync(service);
                }

                // Xóa cửa hàng
                await _unitOfWork._shopRepo.RemoveAsync(shop);

                // Lưu tất cả thay đổi
                return await _unitOfWork.SaveChangeAsync() > 0;
            }
            catch
            {
                // Xử lý lỗi nếu cần thiết
                return false;
            }
        }
    }
}
