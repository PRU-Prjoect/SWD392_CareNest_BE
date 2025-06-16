using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly ApplicationDbContext _context;

        public StaffController(IStaffService staffService, ApplicationDbContext context)
        {
            _staffService = staffService;
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
        [FromQuery] string? fullName,
        [FromQuery] string? shopName,
        [FromQuery] string? gender,
        [FromQuery] string? hiredAt,
        [FromQuery] bool? isActive)
        {
            var result = await _staffService.GetAllAsync(fullName, shopName, gender, hiredAt, isActive);
            return Ok(result);
        }

        // GET: api/Staff/{accountId}
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetById(Guid accountId)
        {
            var result = await _staffService.GetByIdAsync(accountId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffDTO staffDto)
        {
            var success = await _staffService.CreateAsync(staffDto);
            if (!success)
                return BadRequest("Failed to create staff");

            return Ok("Staff created successfully");
        }

        // PUT: api/Staff
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StaffDTO staffDto)
        {
            var success = await _staffService.UpdateAsync(staffDto);
            if (!success)
                return NotFound();

            return Ok("Staff updated successfully");
        }

        // DELETE: api/Staff/{accountId}
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Delete(Guid accountId)
        {
            var success = await _staffService.DeleteAsync(accountId);
            if (!success)
                return NotFound();

            return Ok("Staff deleted successfully");
        }

        // PUT: api/Staff/Cancel/{accountId}
        [HttpPut("Cancel/{accountId}")]
        public async Task<IActionResult> Cancel(Guid accountId)
        {
            var success = await _staffService.CancelStaffAsync(accountId);
            if (!success)
                return NotFound();

            return Ok("Staff account deactivated successfully");
        }

    }
}
