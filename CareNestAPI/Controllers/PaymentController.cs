using BLL.Interfaces;
using BOL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IAccountService _accountService;
        public PaymentController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("generate-vietQR")]
        public async Task<IActionResult> GenerateVietQR(Guid id, int ammount, string? description)
        {
            try
            {
                var result = await _accountService.GenerateVietQr(id, ammount, description);
                if (result == null)
                {
                    return BadRequest(new { message = "Id Invalid" });
                }
                return Ok(new { message = "success", data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }
        [HttpGet("vietQR-Banks")]
        public async Task<IActionResult> GetAllVietQRBank()
        {
            try
            {
                string apiUrl = "https://api.vietqr.io/v2/banks";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                response.EnsureSuccessStatusCode(); 

                string responseBody = await response.Content.ReadAsStringAsync();


                return Ok(responseBody); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }
    }
}
