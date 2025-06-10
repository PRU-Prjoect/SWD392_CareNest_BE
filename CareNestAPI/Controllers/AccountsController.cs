using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using BLL.Interfaces;
using BOL.DTOs;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity.Data;

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
        public async Task<IActionResult> Register([FromForm]AccountRequest accountRequest)
        {
            try
            {
                var result = await _accountService.Register(accountRequest);
                return Ok(new { message = "Registration Success", data = result });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReq loginRequest)
        {
            try
            {
                var result = await _accountService.Login(loginRequest.username, loginRequest.password);
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
        [Authorize]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest)
        {
            try
            {
                var result = await _accountService.ForgotPassword(forgetPasswordRequest.email, forgetPasswordRequest.password);
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
        [Authorize]
        public async Task<IActionResult> SendOtp(SendOtpRequest sendOtpRequest)
        {
            try
            {
                var result = await _accountService.SendOtpAsync(sendOtpRequest.email);
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
        [Authorize]
        public async Task<IActionResult> ConfirmOtp(ConfirmOtpRequest confirmOtpRequest)
        {
            try
            {
                var result = await _accountService.ConfirmOtpAsync(confirmOtpRequest.email, confirmOtpRequest.otp);
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
        [Authorize]
        public async Task<IActionResult> ResetPassword(PasswordResetRequest resetPasswordRequest)
        {
            try
            {
                var result = await _accountService.ResetPassword(resetPasswordRequest.id, resetPasswordRequest.password);
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
        [Authorize]
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
        [HttpPatch("img_url/{id}")]
        public async Task<IActionResult> UpdateImage(Guid id, IFormFile? file)
        {
            try
            {
                var result = await _accountService.UpdateImage(id, file);
                if (result != null)
                {
                    return Ok(new { message = "Image Updated", data = result });
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
