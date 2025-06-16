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
        public async Task<IActionResult> GetServices(
            string name = null,
            bool? isActive = null,
            int? estimatedTime = null,
            Guid? serviceTypeId = null,
            string sortBy = "createdAt") // Tham số sắp xếp
        {
            try
            {
                var services = await _serviceService.GetAllAsync(
                    name, isActive, estimatedTime, serviceTypeId, sortBy);

                return Ok(services);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ServiceDTO serviceDto)
        {
            var success = await _serviceService.CreateAsync(serviceDto);
            if (!success) return BadRequest("Failed to create service.");
            return Ok("Service created successfully.");
        }

        // PUT: api/Service
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ServiceDTO serviceDto)
        {
            var success = await _serviceService.UpdateAsync(serviceDto);
            if (!success) return NotFound("Service not found or update failed.");
            return Ok("Service updated successfully.");
        }

        [HttpPut("Cancel/{id}")]
        [Authorize]
        public async Task<IActionResult> CancelService(Guid id)
        {
            var success = await _serviceService.CancelService(id);

            if (!success)
            {
                return NotFound("Service not found or cannot be canceled due to existing bookings.");
            }

            return Ok("Service canceled successfully.");
        }

        // DELETE: api/Service/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _serviceService.DeleteAsync(id);
            if (!success) return NotFound("Service not found or cannot be deleted");
            return Ok("Service deleted successfully.");
        }

        [HttpPut("UpdateStarAverage/{serviceId}")]
        [Authorize]
        public async Task<IActionResult> UpdateStarAverage(Guid serviceId, int newRating)
        {
            var result = await _serviceService.UpdateStarAverage(serviceId, newRating);

            if (!result)
            {
                return NotFound("Service not found or could not update star average.");
            }

            return Ok("Star average updated successfully.");
        }

        [HttpPut("UpdateAppointmentCount/{serviceId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAppointmentCount(Guid serviceId)
        {
            var result = await _serviceService.UpdateAppointmentCount(serviceId);

            if (!result)
            {
                return NotFound("Service not found or could not update appointment count.");
            }

            return Ok("Appointment count updated successfully.");
        }

    }
}
