using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using BLL.Interfaces;
using BOL.DTOs;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Net.WebRequestMethods;

namespace CareNestAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AccountsController(IAccountService accountService, ITokenService tokenService, ApplicationDbContext context, IEmailService emailService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountDTO accountDTO)
        {
            try
            {
                var result = await _accountService.Register(accountDTO);
                return Ok(new { message = "Registration Success", data = result });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var result = await _accountService.Login(username, password);
                if (result != null)
                {
                    string token = _tokenService.GenerateJWTToken(result);
                    return Ok(new { message = "Login Success", data = token });
                }
                else
                {
                    return BadRequest(new { message = "Wrong username or password" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }
        }
        [HttpPatch("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email, string password)
        {
            try
            {
                var result = await _accountService.ForgotPassword(email, password);
                if (result)
                {
                    return Ok(new { message = "Password changed" });
                }
                else
                {
                    return BadRequest(new { message = "Email is invalid" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }
        }
        [HttpPatch("send_email")]
        public async Task<IActionResult> SendOtp(string email)
        {
            try
            {
                var result = await _accountService.SendOtpAsync(email);
                if (result)
                {
                    return Ok(new { message = "Otp Sent" });
                }
                else
                {
                    return BadRequest(new { message = "Email is invalid" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }
        }
        [HttpPatch("confirm_email")]
        public async Task<IActionResult> ConfirmOtp(string email, string otp)
        {
            try
            {
                var result = await _accountService.ConfirmOtpAsync(email, otp);
                if (result)
                {
                    return Ok(new { message = "Otp Confirm" });
                }
                else
                {
                    return BadRequest(new { message = "Otp is invalid" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }

        }
        [HttpPatch("reset-password/{id}")]
        public async Task<IActionResult> ResetPassword(string id, string password)
        {
            try
            {
                var result = await _accountService.ResetPassword(id, password);
                if (result)
                {
                    return Ok(new { message = "Password changed" });
                }
                else
                {
                    return BadRequest(new { message = "id is invalid" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }

        }
        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> ActivateAccount(string id)
        {
            try
            {
                var result = await _accountService.ActivateAccount(id);
                if (result)
                {
                    return Ok(new { message = "Account actvated" });
                }
                else
                {
                    return BadRequest(new { message = "id is invalid" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }

        }
    }
}
