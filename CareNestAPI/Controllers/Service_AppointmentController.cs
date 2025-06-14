using BLL.Interfaces;
using BLL.Services;
using BOL.DTOs;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Service_AppointmentController : ControllerBase
    {
        private readonly IService_AppointmentService _service_AppointmentService;
        private readonly ApplicationDbContext _context;

        public Service_AppointmentController(IService_AppointmentService service_AppointmentService, ApplicationDbContext context)
        {
            _service_AppointmentService = service_AppointmentService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] Guid? serviceId,
            [FromQuery] Guid? appointmentId,
            [FromQuery] DateTime? startDate)
        {
            var result = await _service_AppointmentService.GetAllAsync(serviceId, appointmentId, startDate);
            return Ok(result);
        }

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetById(Guid serviceId)
        {
            var result = await _service_AppointmentService.GetByIdAsync(serviceId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Service_AppointmentDTO dto)
        {
            var success = await _service_AppointmentService.CreateAsync(dto);
            if (!success) return BadRequest("Failed to create service appointment.");
            return Ok("Service appointment created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Service_AppointmentDTO dto)
        {
            var success = await _service_AppointmentService.UpdateAsync(dto);
            if (!success) return NotFound("Service appointment not found or update failed.");
            return Ok("Service appointment updated successfully.");
        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> Delete(Guid serviceId)
        {
            var success = await _service_AppointmentService.DeleteAsync(serviceId);
            if (!success) return NotFound("Service appointment not found.");
            return Ok("Service appointment deleted successfully.");
        }
    }
}
