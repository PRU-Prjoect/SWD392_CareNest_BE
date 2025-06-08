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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICloudinaryService _cloudinaryService;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _cloudinaryService = cloudinaryService;
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
            if (accountDTO.img != null)
            {
                CloudinaryDTO cloudinaryDTO = await _cloudinaryService.UploadImage(accountDTO.img);
                accountDTO.img_url = cloudinaryDTO.url;
                accountDTO.img_url_id = cloudinaryDTO.publicId;
            }
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

        public async Task<AccountDTO> UpdateImage(Guid id, IFormFile? file)
        {
            CloudinaryDTO cloudinaryDTO = new CloudinaryDTO();
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }

            if (file == null)
            {
                if (account.img_url_id != null)
                {
                    var result = await _cloudinaryService.DeleteImage(account.img_url_id);
                }
                account.img_url = null;
                account.img_url_id = null;
            }
            else if (account.img_url_id == null)
            {
                cloudinaryDTO = await _cloudinaryService.UploadImage(file);
                account.img_url = cloudinaryDTO.url;
                account.img_url_id = cloudinaryDTO.publicId;
            }
            else
            {
                cloudinaryDTO = await _cloudinaryService.UpdateImage(file, account.img_url_id);
                account.img_url = cloudinaryDTO.url;
                account.img_url_id = cloudinaryDTO.publicId;
            }


            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<AccountDTO>(account);

        }
    }
}
