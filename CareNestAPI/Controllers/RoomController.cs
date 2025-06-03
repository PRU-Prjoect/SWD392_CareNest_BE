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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly ApplicationDbContext _context;
        public RoomController(IRoomService roomService, ApplicationDbContext context)
        {
            _roomService = roomService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roomService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _roomService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomDTO roomDto)
        {
            var success = await _roomService.CreateAsync(roomDto);
            if (!success) return BadRequest("Failed to create room.");
            return Ok("Room created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoomDTO roomDto)
        {
            var success = await _roomService.UpdateAsync(roomDto);
            if (!success) return NotFound("Room not found or update failed.");
            return Ok("Room updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _roomService.DeleteAsync(id);
            if (!success) return NotFound("Room not found.");
            return Ok("Room deleted successfully.");
        }
    }
}
