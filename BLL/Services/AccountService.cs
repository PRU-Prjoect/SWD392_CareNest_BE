using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Web;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ICloudinaryService cloudinaryService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _cloudinaryService = cloudinaryService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<AccountResponse> Register(AccountRequest accountRequest)
        {
            accountRequest.password = BCrypt.Net.BCrypt.HashPassword(accountRequest.password);
            var account = _mapper.Map<Account>(accountRequest);
            account.role = Role.Guest;
            account.is_active = true;
            if (accountRequest.img != null)
            {
                CloudinaryDTO cloudinaryDTO = await _cloudinaryService.UploadImage(accountRequest.img);
                account.img_url = cloudinaryDTO.url;
                account.img_url_id = cloudinaryDTO.publicId;
            }

            await _unitOfWork._accountRepo.AddAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<AccountResponse>(account);
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

        public async Task<bool> ResetPassword(Guid id, string password)
        {
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
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

        public async Task<bool> ActivateAccount(Guid id)
        {
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return false;
            }
            account.is_active = true;
            await _unitOfWork._accountRepo.UpdateAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<AccountRequest> UpdateImage(Guid id, IFormFile? file)
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
            return _mapper.Map<AccountRequest>(account);

        }

        public async Task<List<Account>> GetAllAccount()
        {
            List<Account> accountList = await _unitOfWork._accountRepo.GetAllAsync();
            return accountList;
        }

        public async Task<Account> GetAccountById(Guid id)
        {
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<Account> GetLoginAccount()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid id = Guid.Parse(claim);
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<bool> DeleteAccount(Guid id)
        {
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return false;
            }
            await _unitOfWork._accountRepo.RemoveAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<Account> UpdateAccount(Guid id, UpdateAccountRequest updateAccountRequest)
        {
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }
            account = _mapper.Map(updateAccountRequest, account);
            await _unitOfWork._accountRepo.UpdateAsync(account);
            await _unitOfWork.SaveChangeAsync();
            return account;
        }
        public async Task<string> GenerateVietQr(Guid id, int ammount, string? description)
        {
            var account = await _unitOfWork._accountRepo.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }
            if (account.BANK_ACCOUNT_NO == null || account.BANK_ID == null || account.BANK_ACCOUNT_NAME == null)
            {
                return null;
            }

            var uriBuilder = new UriBuilder("https://img.vietqr.io")
            {
                Path = $"image/{account.BANK_ID}-{account.BANK_ACCOUNT_NO}-compact2.jpg"
            };

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["amount"] = ammount.ToString();
            if (!string.IsNullOrEmpty(description))
            {
                query["addInfo"] = description;
            }
            query["accountName"] = account.BANK_ACCOUNT_NAME.ToString();

            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();


        }

    }
}
