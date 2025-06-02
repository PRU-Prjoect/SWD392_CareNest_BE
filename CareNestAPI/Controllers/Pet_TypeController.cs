using BLL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> GetAllPetTypes()
        {
            try
            {
                var petTypes = await _pet_TypeService.GetAllAsync();
                return Ok(petTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }
    }
}
