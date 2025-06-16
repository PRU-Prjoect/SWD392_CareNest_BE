using BOL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountResponse> Register(AccountRequest accountRequest);
        public Task<Account?> Login(string username, string password);
        public Task<bool> ForgotPassword(string email, string password);
        public Task<bool> ResetPassword(Guid id, string password);
        public Task<bool> SendOtpAsync(string email);
        public Task<bool> ConfirmOtpAsync(string email, string otp);
        public Task<bool> ActivateAccount(Guid id);
        public Task<AccountRequest> UpdateImage(Guid id, IFormFile? file);
        public Task<List<Account>> GetAllAccount();
        public Task<Account> GetAccountById(Guid id);
        public Task<Account> GetLoginAccount();
        public Task<bool> DeleteAccount(Guid id);
        public Task<Account> UpdateAccount(Guid id, UpdateAccountRequest updateAccountRequest);
    }
}
