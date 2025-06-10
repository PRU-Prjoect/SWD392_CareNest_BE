using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly ApplicationDbContext _context;

        public ShopController(IShopService shopService, ApplicationDbContext context)
        {
            _shopService = shopService;
            _context = context;
        }

        // GET: api/shop
        [HttpGet]
        public async Task<IActionResult> GetAllShops(string? name = null, bool? status = null)
        {
            var shops = await _shopService.GetAllAsync(name, status);
            return Ok(shops);
        }

        // GET: api/shop/{accountId}
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetShopByAccountId(Guid accountId)
        {
            var shop = await _shopService.GetByIdAsync(accountId);
            if (shop == null) return NotFound();
            return Ok(shop);
        }

        // POST: api/shop/register/{accountId}
        [HttpPost("register")]
        public async Task<IActionResult> RegisterShop([FromBody] ShopRequest shopDto)
        {
            var result = await _shopService.RegisterShopAsync(shopDto);
            if (!result) return BadRequest("Shop already exists or cannot be created.");
            return Ok("Shop registered successfully.");
        }

        // PUT: api/shop
        [HttpPut]
        public async Task<IActionResult> UpdateShop([FromBody] ShopRequest shopDto)
        {
            var result = await _shopService.UpdateAsync(shopDto);
            if (!result) return NotFound("Shop not found or cannot be updated.");
            return Ok("Shop updated successfully.");
        }

        // DELETE: api/shop/{accountId}
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteShop(Guid accountId)
        {
            var result = await _shopService.DeleteAsync(accountId);
            if (!result) return NotFound("Shop not found or cannot be deleted.");
            return Ok("Shop deleted successfully.");
        }
    }
}
