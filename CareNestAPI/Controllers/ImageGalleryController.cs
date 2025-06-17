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
    public class ImageGalleryController : ControllerBase
    {
        private readonly IImageGalleryService _imageGalleryService;
        private readonly ApplicationDbContext _context;

        public ImageGalleryController(IImageGalleryService imageGalleryService, ApplicationDbContext context)
        {
            _imageGalleryService = imageGalleryService;
            _context = context;
        }

        // GET: api/ImageGallery
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? ownerId = null, [FromQuery] string? nameFilter = null)
        {
            var result = await _imageGalleryService.GetAllAsync(ownerId, nameFilter);
            return Ok(result);
        }

        // GET: api/ImageGallery/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _imageGalleryService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/ImageGallery
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ImageGalleryRequest request)
        {
            var created = await _imageGalleryService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
        }

        // PUT: api/ImageGallery
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ImageGalleryRequest request)
        {
            var updated = await _imageGalleryService.UpdateAsync(request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/ImageGallery/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _imageGalleryService.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok("Delete Successfully");
        }
    }
}
