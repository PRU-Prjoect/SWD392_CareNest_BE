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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationDbContext _context;

        public CustomerController(ICustomerService customerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string? name,
            [FromQuery] string? gender,
            [FromQuery] string? email)
        {
            var customers = await _customerService.GetAllAsync(name, gender, email);
            return Ok(customers);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetById(Guid accountId)
        {
            var customer = await _customerService.GetByIdAsync(accountId);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost("{accountId}")]
        public async Task<IActionResult> Create(Guid accountId, [FromBody] CustomerDTO customerDto)
        {
            var result = await _customerService.CreateAsync(accountId, customerDto);
            if (!result)
                return BadRequest("Create failed.");

            return Ok("Customer created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerDTO customerDto)
        {
            var result = await _customerService.UpdateAsync(customerDto);
            if (!result)
                return BadRequest("Update failed.");

            return Ok("Customer updated successfully.");
        }
    }
}
