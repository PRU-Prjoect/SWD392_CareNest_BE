using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetAll(
            Guid? customerId = null,
            AppointmentStatus? status = null,
            DateTime? startTime = null)
        {
            var result = await _appointmentsService.GetAllAsync(customerId, status, startTime);
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
        [HttpGet("report")]
        public async Task<IActionResult> GetAppointmentReport()
        {
            try
            {
                var result = await _appointmentsService.GetAppointmentreport();
                return Ok(new { message = "Success", data = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "An error occurred" });
            }
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentsDTO appointmentDto)
        {
            if (appointmentDto == null)
                return BadRequest("Invalid appointment data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _appointmentsService.CreateAsync(appointmentDto);

            if (result == null)
                return BadRequest("Failed to create appointment.");

            return CreatedAtAction(nameof(GetById), new { id = result.id }, result);
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
