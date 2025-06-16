using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Room_BookingController : ControllerBase
    {
        private readonly IRoom_BookingService _room_BookingService;
        private readonly ApplicationDbContext _context;
        public Room_BookingController(IRoom_BookingService room_BookingService, ApplicationDbContext context)
        {
            _room_BookingService = room_BookingService;
            _context = context;
        }

        // GET: api/RoomBooking
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid? roomDetailId,
            [FromQuery] Guid? customerId,
            [FromQuery] DateTime? checkInDate,
            [FromQuery] DateTime? checkOutDate,
            [FromQuery] bool? status)
        {
            var result = await _room_BookingService.GetAllAsync(roomDetailId, customerId, checkInDate, checkOutDate, status);
            return Ok(result);
        }

        // GET: api/RoomBooking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _room_BookingService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/RoomBooking
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Room_BookingDTO dto)
        {
            var success = await _room_BookingService.CreateAsync(dto);
            if (!success)
                return BadRequest("Failed to create room booking.");

            return Ok("Room booking created successfully.");
        }

        // PUT: api/RoomBooking
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Room_BookingDTO dto)
        {
            var success = await _room_BookingService.UpdateAsync(dto);
            if (!success)
                return NotFound("Room booking not found.");

            return Ok("Room booking updated successfully.");
        }

        // DELETE: api/RoomBooking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _room_BookingService.DeleteAsync(id);
            if (!success)
                return NotFound("Room booking not found.");

            return Ok("Room booking deleted successfully.");
        }
    }
}
