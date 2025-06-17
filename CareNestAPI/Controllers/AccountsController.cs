using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register([FromForm] AccountRequest accountRequest)
        {
            try
            {
                var result = await _accountService.Register(accountRequest);
                return Ok(new { message = "Registration Success", data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }
        }
        [HttpPatch("forget-password")]
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }
        }
        [HttpPatch("send_email")]
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }
        }
        [HttpPatch("confirm_email")]
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }
        /// <summary>
        /// dành cho shop cấp tài khoản nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPatch("reset-password/{id}")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromRoute] Guid id, [FromBody] PasswordResetRequest resetPasswordRequest)
        {
            try
            {
                var result = await _accountService.ResetPassword(id, resetPasswordRequest.password);
                if (result)
                {
                    return Ok(new { message = "Password changed" });
                }
                else
                {
                    return BadRequest(new { message = "id is invalid" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpPatch("activate/{id}")]
        [Authorize]
        public async Task<IActionResult> ActivateAccount(Guid id)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                var result = await _accountService.GetAllAccount();
                return Ok(new { message = "success", data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            try
            {
                var result = await _accountService.GetAccountById(id);
                if (result != null)
                {
                    return Ok(new { message = "Success", data = result });
                }
                else
                {
                    return BadRequest(new { message = "id is invalid" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpGet("get-login")]
        [Authorize]
        public async Task<IActionResult> GetLoginAccount()
        {
            try
            {
                var result = await _accountService.GetLoginAccount();
                if (result != null)
                {
                    return Ok(new { message = "Success", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Please Login" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }
        [HttpDelete("delete-account/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            try
            {
                var result = await _accountService.DeleteAccount(id);
                if (result != null)
                {
                    return Ok(new { message = "Success", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Invalid Id" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpPatch("update-account/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] UpdateAccountRequest updateAccountRequest)
        {
            try
            {
                var result = await _accountService.UpdateAccount(id, updateAccountRequest);
                if (result != null)
                {
                    return Ok(new { message = "Success", data = result });
                }
                else
                {
                    return BadRequest(new { message = "id is invalid" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }
    }
}
