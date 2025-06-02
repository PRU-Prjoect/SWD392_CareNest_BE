using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<StaffDTO>> GetAllAsync()
        {
            var staffs = await _unitOfWork._staffRepo.GetAllAsync();
            var staffDTOs = _mapper.Map<List<StaffDTO>>(staffs);
            return staffDTOs;
        }

        public async Task<StaffDTO> GetByIdAsync(Guid accountId)
        {
            var staff = await _unitOfWork._staffRepo.GetByIdAsync(accountId);
            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<bool> CreateAsync(Guid accountId, Guid shopId, StaffDTO staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            staff.account_id = accountId;
            staff.shop_id = shopId;

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
            var staff = await _unitOfWork._staffRepo.GetByIdAsync(accountId);
            if (staff == null) return false;

            //staff.account.is_active = false; 
            await _unitOfWork._staffRepo.UpdateAsync(staff);

            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
