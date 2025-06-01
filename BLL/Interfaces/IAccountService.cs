using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountDTO> Register(AccountDTO accountDTO);
        public Task<Account?> Login(string username, string password);
        public Task<bool> ForgotPassword(string email, string password);
        public Task<bool> ResetPassword(string id, string password);
        public Task<bool> SendOtpAsync(string email);
        public Task<bool> ConfirmOtpAsync(string email, string otp);
        public Task<bool> ActivateAccount(string username);
    }
}
