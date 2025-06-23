using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }



        [HttpPost("add/")]
        public async Task<IActionResult> addServiceToCart([FromBody] CartRequest cartRequest)
        {
            try
            {
                var result = await _cartService.AddServiceToCart(cartRequest);
                return Ok(new { message = "success", data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpGet("get/{customerId}")]
        public async Task<IActionResult> GetAllServiceFromCustomerCart([FromRoute] Guid customerId)
        {
            try
            {
                var result = await _cartService.GetAllCartServicesByCustomerId(customerId);

                return Ok(new { message = "Success", data = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }

        [HttpDelete("delete/")]
        public async Task<IActionResult> DeleteAccount([FromBody] CartRequest cartRequest)
        {
            try
            {
                var result = await _cartService.RemoveServiceFromCart(cartRequest);
                return Ok(new { message = "Success", data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }

        }
    }
}
