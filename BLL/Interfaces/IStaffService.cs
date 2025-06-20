﻿using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IStaffService
    {
        Task<List<StaffResponse>> GetAllAsync(
                string? fullName = null,
                string? shopName = null,
                string? gender = null,
                string? hiredAt = null,
                bool? isActive = null);
        Task<StaffDTO> GetByIdAsync(Guid staffId);
        Task<bool> CreateAsync(StaffDTO staffDto);
        Task<bool> UpdateAsync(StaffDTO staffDto);
        Task<bool> DeleteAsync(Guid staffId);
        Task<bool> CancelStaffAsync(Guid staffId);
    }
}
