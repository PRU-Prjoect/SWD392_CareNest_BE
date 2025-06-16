using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<StaffResponse>> GetAllAsync(
                string? fullName = null,
                string? shopName = null,
                string? gender = null,
                string? hiredAt = null,
                bool? isActive = null)
        {
            // Lấy toàn bộ danh sách trước (đã load vào RAM)
            var staffList = await _unitOfWork._staffRepo.GetAllStaff(); // List hoặc IEnumerable

            // Bắt đầu filter in-memory
            var filtered = staffList.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var loweredName = fullName.ToLower();
                filtered = filtered.Where(s => s.full_name != null && s.full_name.ToLower().Contains(loweredName));
            }

            if (!string.IsNullOrWhiteSpace(shopName))
            {
                var loweredShopName = shopName.ToLower();
                filtered = filtered.Where(s => s.shop != null && s.shop.name != null && s.shop.name.ToLower().Contains(loweredShopName));
            }

            if (!string.IsNullOrWhiteSpace(gender))
            {
                filtered = filtered.Where(s => s.gender != null && s.gender.ToLower() == gender.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(hiredAt))
            {
                filtered = filtered.Where(s =>
                    s.hired_at?.Equals(hiredAt, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (isActive.HasValue)
            {
                filtered = filtered.Where(s => s.account != null && s.account.is_active == isActive.Value);
            }

            return _mapper.Map<List<StaffResponse>>(filtered.ToList());
        }

        public async Task<StaffDTO> GetByIdAsync(Guid accountId)
        {
            var staff = await _unitOfWork._staffRepo.GetByIdAsync(accountId);
            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<bool> CreateAsync(StaffDTO staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);

            // Không gán staff.account hoặc staff.shop ở đây!
            await _unitOfWork._staffRepo.AddAsync(staff);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }


        public async Task<bool> UpdateAsync(StaffDTO staffDto)
        {

            var staff = _mapper.Map<Staff>(staffDto);

            await _unitOfWork._staffRepo.UpdateAsync(staff);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        //
        public async Task<bool> DeleteAsync(Guid accountId)
        {
            var staff = await _unitOfWork._staffRepo.GetByIdAsync(accountId);
            if (staff == null) return false;

            await _unitOfWork._staffRepo.RemoveAsync(staff);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> CancelStaffAsync(Guid accountId)
        {
            var staff = await _unitOfWork._staffRepo.GetStaffByIdAsync(accountId);
            if (staff == null) return false;

            staff.account.is_active = false;
            await _unitOfWork._staffRepo.UpdateAsync(staff);

            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
