using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pet_Service_RoomController : ControllerBase
    {
        private readonly IPet_Service_RoomService _pet_Service_RoomService;
        private readonly ApplicationDbContext _context;

        public Pet_Service_RoomController(IPet_Service_RoomService pet_Service_RoomService, ApplicationDbContext context)
        {
            _pet_Service_RoomService = pet_Service_RoomService;
            _context = context;
        }

        // GET: api/pet_service_room
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid? ownerId = null, Guid? petTypeId = null, bool? isService = null)
        {
            try
            {
                var petServiceRooms = await _pet_Service_RoomService.GetAllAsync(ownerId, petTypeId, isService);
                return Ok(petServiceRooms);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // GET: api/pet_service_room/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var petServiceRoom = await _pet_Service_RoomService.GetByIdAsync(id);
            if (petServiceRoom == null) return NotFound();

            return Ok(petServiceRoom);
        }

        // POST: api/pet_service_room
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Pet_Service_RoomRequest petServiceRoomRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _pet_Service_RoomService.CreateAsync(petServiceRoomRequest);
                if (!success)
                {
                    return BadRequest("Failed to create pet service room.");
                }
                return Ok("Pet service room created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating pet service room: {ex.Message}");
            }
        }

        // PUT: api/pet_service_room
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Pet_Service_RoomRequest petServiceRoomRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _pet_Service_RoomService.UpdateAsync(petServiceRoomRequest);
            if (!success) return NotFound("Pet service room not found or update failed.");

            return NoContent();
        }

        // DELETE: api/pet_service_room/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var success = await _pet_Service_RoomService.DeleteAsync(id);
            if (!success) return NotFound("Pet service room not found.");

            return Ok("Pet service room deleted successfully.");
        }
    }
}
