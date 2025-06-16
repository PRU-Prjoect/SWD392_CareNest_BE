using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Service_TypeController : ControllerBase
    {
        private readonly IService_TypeService _service_TypeService;
        private readonly ApplicationDbContext _context;

        public Service_TypeController(IService_TypeService service_TypeService, ApplicationDbContext context)
        {
            _service_TypeService = service_TypeService;
            _context = context;
        }

        // GET: api/service-type
        [HttpGet]
        public async Task<IActionResult> GetAllServiceTypes()
        {
            try
            {
                var serviceTypes = await _service_TypeService.GetAllAsync();
                return Ok(serviceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // GET: api/service-type/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceTypeById(Guid id)
        {
            try
            {
                var serviceType = await _service_TypeService.GetByIdAsync(id);
                if (serviceType == null)
                {
                    return NotFound();
                }
                return Ok(serviceType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // POST: api/service-type
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateServiceType([FromForm] Service_TypeRequest serviceTypeDTO)
        {
            try
            {
                var result = await _service_TypeService.CreateAsync(serviceTypeDTO);
                return Ok(new { message = "Registration Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating data: {ex.Message}");
            }
        }

        // PUT: api/service-type/{id}
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateServiceType([FromForm] Service_TypeRequest serviceTypeDTO)
        {
            try
            {
                var result = await _service_TypeService.UpdateAsync(serviceTypeDTO);
                return Ok(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating data: {ex.Message}");
            }
        }

        // DELETE: api/service-type/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteServiceType(Guid id)
        {
            try
            {
                var result = await _service_TypeService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting data: {ex.Message}");
            }
        }
    }
}
