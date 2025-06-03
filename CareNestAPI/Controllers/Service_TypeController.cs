using BLL.Interfaces;
using BLL.Services;
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
        public async Task<IActionResult> CreateServiceType(Service_TypeDTO serviceTypeDTO)
        {
            try
            {
                var result = await _service_TypeService.CreateAsync(serviceTypeDTO);
                if (result)
                {
                    return CreatedAtAction(nameof(GetServiceTypeById), new { id = serviceTypeDTO.id }, serviceTypeDTO);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating data: {ex.Message}");
            }
        }

        // PUT: api/service-type/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceType(Guid id, Service_TypeDTO serviceTypeDTO)
        {
            if (id != serviceTypeDTO.id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _service_TypeService.UpdateAsync(serviceTypeDTO);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating data: {ex.Message}");
            }
        }

        // DELETE: api/service-type/{id}
        [HttpDelete("{id}")]
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
