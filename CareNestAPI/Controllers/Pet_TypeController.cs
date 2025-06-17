using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pet_TypeController : ControllerBase
    {
        private readonly IPet_TypeService _pet_TypeService;
        private readonly ApplicationDbContext _context;
        public Pet_TypeController(IPet_TypeService pet_TypeService, ApplicationDbContext context)
        {
            _pet_TypeService = pet_TypeService;
            _context = context;
        }

        // GET: api/pet-type
        [HttpGet]
        public async Task<IActionResult> GetAllPetTypes(string? nameFilter = null)
        {
            try
            {
                var petTypes = await _pet_TypeService.GetAllAsync(nameFilter);
                return Ok(petTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // GET: api/pet-type/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetTypeById(Guid id)
        {
            try
            {
                var petType = await _pet_TypeService.GetByIdAsync(id);
                if (petType == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return Ok(petType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // POST: api/pet-type
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePetType([FromBody] Pet_TypeDTO petTypeDTO)
        {
            if (petTypeDTO == null)
            {
                return BadRequest("Pet type data is null.");
            }

            try
            {
                var result = await _pet_TypeService.CreateAsync(petTypeDTO);
                if (!result)
                {
                    return BadRequest("Could not create pet type.");
                }
                return CreatedAtAction(nameof(GetPetTypeById), new { id = petTypeDTO.id }, petTypeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating pet type: {ex.Message}");
            }
        }

        // PUT: api/pet-type/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePetType(Guid id, [FromBody] Pet_TypeDTO petTypeDTO)
        {
            if (petTypeDTO == null || petTypeDTO.id != id)
            {
                return BadRequest("Pet type data is invalid.");
            }

            try
            {
                var result = await _pet_TypeService.UpdateAsync(petTypeDTO);
                if (!result)
                {
                    return NotFound(); // 404 Not Found
                }
                return Ok("Update Successfully"); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating pet type: {ex.Message}");
            }
        }

        // DELETE: api/pet-type/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePetType(Guid id)
        {
            try
            {
                var result = await _pet_TypeService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(); // 404 Not Found
                }
                return Ok("Delete Successfully"); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting pet type: {ex.Message}");
            }
        }
    }
}
