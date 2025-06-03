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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly ApplicationDbContext _context;

        public ServiceController(IServiceService serviceService, ApplicationDbContext context)
        {
            _serviceService = serviceService;
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(services);
        }

        // GET: api/Service/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound("Service not found.");
            return Ok(service);
        }

        // POST: api/Service
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDTO serviceDto)
        {
            var success = await _serviceService.CreateAsync(serviceDto);
            if (!success) return BadRequest("Failed to create service.");
            return Ok("Service created successfully.");
        }

        // PUT: api/Service
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ServiceDTO serviceDto)
        {
            var success = await _serviceService.UpdateAsync(serviceDto);
            if (!success) return NotFound("Service not found or update failed.");
            return Ok("Service updated successfully.");
        }

        // DELETE: api/Service/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _serviceService.DeleteAsync(id);
            if (!success) return NotFound("Service not found.");
            return Ok("Service deleted successfully.");
        }

    }
}
