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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ApplicationDbContext _context;

        public NotificationController(INotificationService notificationService, ApplicationDbContext context)
        {
            _notificationService = notificationService;
            _context = context;
        }


        // GET: api/notification?receiverId=...&description=...&isRead=...
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications(
            [FromQuery] Guid? receiverId,
            [FromQuery] string? description,
            [FromQuery] bool? isRead)
        {
            try
            {
                var notifications = await _notificationService.GetAllAsync(receiverId, description, isRead);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving notifications: {ex.Message}");
            }
        }

        // GET: api/notification/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            try
            {
                var notification = await _notificationService.GetByIdAsync(id);
                if (notification == null)
                {
                    return NotFound();
                }
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving notification: {ex.Message}");
            }
        }

        // POST: api/notification
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDTO notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _notificationService.CreateAsync(notificationDto);
                return CreatedAtAction(nameof(GetNotificationById), new { id = created.id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating notification: {ex.Message}");
            }
        }

        // PUT: api/notification/{id}
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationDTO notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _notificationService.UpdateAsync(notificationDto);
                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating notification: {ex.Message}");
            }
        }

        // DELETE: api/notification/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            try
            {
                var success = await _notificationService.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting notification: {ex.Message}");
            }
        }
    }
}
