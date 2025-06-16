using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ApplicationDbContext _context;

        public HotelController(IHotelService hotelService, ApplicationDbContext context)
        {
            _hotelService = hotelService;
            _context = context;
        }

        // GET: api/hotel
        [HttpGet]
        public async Task<IActionResult> GetAllHotels(Guid? shopId = null, bool? isActive = null, string? nameFilter = null)
        {
            try
            {
                var hotels = await _hotelService.GetAllAsync(shopId, isActive, nameFilter);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // GET: api/hotel/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            try
            {
                var hotel = await _hotelService.GetByIdAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // POST: api/hotel
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTO hotelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _hotelService.CreateAsync(hotelDto);
                if (!result)
                {
                    return BadRequest("Unable to create hotel.");
                }
                return CreatedAtAction(nameof(GetHotelById), new { id = hotelDto.id }, hotelDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating hotel: {ex.Message}");
            }
        }

        // PUT: api/hotel/{id}
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelDTO hotelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _hotelService.UpdateAsync(hotelDto);
                if (!result)
                {
                    return BadRequest("Unable to update hotel.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating hotel: {ex.Message}");
            }
        }

        // DELETE: api/hotel/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            try
            {
                var result = await _hotelService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting hotel: {ex.Message}");
            }
        }
    }
}
