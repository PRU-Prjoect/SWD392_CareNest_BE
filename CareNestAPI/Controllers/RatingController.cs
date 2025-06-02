using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly ApplicationDbContext _context;
        public RatingController(IRatingService ratingService, ApplicationDbContext context)
        {
            _ratingService = ratingService;
            _context = context;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ratingService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Rating/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _ratingService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Rating
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RatingDTO ratingDto)
        {
            var success = await _ratingService.CreateAsync(ratingDto);
            if (!success) return BadRequest("Failed to create rating.");
            return Ok("Rating created successfully.");
        }

        // PUT: api/Rating
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RatingDTO ratingDto)
        {
            var success = await _ratingService.UpdateAsync(ratingDto);
            if (!success) return NotFound("Rating not found or update failed.");
            return Ok("Rating updated successfully.");
        }

        // DELETE: api/Rating/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _ratingService.DeleteAsync(id);
            if (!success) return NotFound("Rating not found.");
            return Ok("Rating deleted successfully.");
        }
    }
}
