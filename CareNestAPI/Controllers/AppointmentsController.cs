using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly ApplicationDbContext _context;

        public AppointmentsController(IAppointmentsService appointmentsService, ApplicationDbContext context)
        {
            _appointmentsService = appointmentsService;
            _context = context;
        }

        // GET: api/appointments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentsService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/appointments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _appointmentsService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentsDTO appointmentDto)
        {
            if (appointmentDto == null) return BadRequest("Invalid appointment data.");

            var success = await _appointmentsService.CreateAsync(appointmentDto);
            if (!success) return BadRequest("Failed to create appointment.");

            return Ok("Appointment created successfully.");
        }

        // PUT: api/appointments
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AppointmentsDTO appointmentDto)
        {
            if (appointmentDto == null) return BadRequest("Invalid appointment data.");

            var success = await _appointmentsService.UpdateAsync(appointmentDto);
            if (!success) return NotFound("Appointment not found or update failed.");

            return Ok("Appointment updated successfully.");
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _appointmentsService.DeleteAsync(id);
            if (!success) return NotFound("Appointment not found.");

            return Ok("Appointment deleted successfully.");
        }
    }
}
