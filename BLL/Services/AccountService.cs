using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Mapper;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }



        public async Task<Account?> Login(string username, string password)
        {
            var account = await _unitOfWork._accountRepo.GetByUsernameAsync(username);
            if (account == null)
            {
                return null;
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, account.password);
            if (!isPasswordValid)
            {
                return null;
            }
            else
            {
                return account;
            }
        }

        public async Task<AccountDTO> Register(AccountDTO accountDTO)
        {
                accountDTO.password = BCrypt.Net.BCrypt.HashPassword(accountDTO.password);
                var account = _mapper.Map<Account>(accountDTO);
                account.role = Role.Guest;
                account.is_active = true;
                await _unitOfWork._accountRepo.AddAsync(account);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<AccountDTO>(account);
        }
        public async Task<bool> ForgotPassword(string email, string password)
        {
            var account = await _unitOfWork._accountRepo.GetByEmailAsync(email);
            if (account == null)
            {
                return false;
            }

            account.password = BCrypt.Net.BCrypt.HashPassword(password);
            account.updated_at = DateTime.UtcNow;

            await _unitOfWork._accountRepo.UpdateAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> SendOtpAsync(string email)
        {
            var account = await _unitOfWork._accountRepo.GetByEmailAsync(email);
            if (account == null)
            {
                return false;
            }

            Random random = new Random();
            var otp = random.Next(100000, 1000000).ToString();
            account.otp = otp;
            account.otpExpired = DateTime.UtcNow.AddMinutes(10);

            await _unitOfWork._accountRepo.UpdateAsync(account);
            await _unitOfWork.SaveChangeAsync();

            string subject = "OTP Verify";
            string message = $"Your OTP is: {otp} .This OTP is valid for 10 minutes.";
            await _emailService.SendEmailAsync(email, subject, message);

            return true;
        }

        public async Task<bool> ConfirmOtpAsync(string email, string otp)
        {
            var account = await _unitOfWork._accountRepo.GetByEmailAsync(email);

            if (account == null || account.otp != otp || account.otpExpired <= DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ResetPassword(string id, string password)
        {
            var account = await _unitOfWork._accountRepo.GetByUsernameAsync(id);
            if (account == null)
            {
                return false;
            }

            account.password = BCrypt.Net.BCrypt.HashPassword(password);
            account.updated_at = DateTime.UtcNow;

            await _unitOfWork._accountRepo.UpdateAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> ActivateAccount(string username)
        {
            var account = await _unitOfWork._accountRepo.GetByUsernameAsync(username);
            if (account == null)
            {
                return false;
            }
            account.is_active = true;
            await _unitOfWork._accountRepo.UpdateAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
