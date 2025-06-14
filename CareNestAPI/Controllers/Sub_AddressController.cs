using BLL.Interfaces;
using BLL.Services;
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
    public class Sub_AddressController : ControllerBase
    {
        private readonly ISub_AddressService _sub_AddressService;
        private readonly ApplicationDbContext _context;

        public Sub_AddressController(ISub_AddressService sub_AddressService, ApplicationDbContext context)
        {
            _sub_AddressService = sub_AddressService;
            _context = context;
        }

        // GET: api/Sub_Address
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid? shopId = null, string? addressName = null, bool? isDefault = null)
        {
            try
            {
                var subAddresses = await _sub_AddressService.GetAllAsync(shopId, addressName, isDefault);
                return Ok(subAddresses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // GET: api/Sub_Address/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sub_AddressService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Sub_Address
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sub_AddressDTO subAddressDto)
        {
            var success = await _sub_AddressService.CreateAsync(subAddressDto);
            if (!success) return BadRequest("Failed to create sub-address.");
            return Ok("Sub-address created successfully.");
        }

        // PUT: api/Sub_Address
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Sub_AddressDTO subAddressDto)
        {
            var success = await _sub_AddressService.UpdateAsync(subAddressDto);
            if (!success) return NotFound("Sub-address not found or update failed.");
            return Ok("Sub-address updated successfully.");
        }

        // DELETE: api/Sub_Address/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _sub_AddressService.DeleteAsync(id);
            if (!success) return NotFound("Sub-address not found.");
            return Ok("Sub-address deleted successfully.");
        }
    }
}
