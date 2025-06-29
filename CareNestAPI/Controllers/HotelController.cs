﻿using BLL.Interfaces;
using BOL.DTOs;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareNestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ApplicationDbContext _context;

        public HotelController(IHotelService hotelService, ApplicationDbContext context)
        {
            _hotelService = hotelService;
            _context = context;
        }

        // GET: api/hotel
        [HttpGet]
        public async Task<IActionResult> GetAllHotels(Guid? shopId = null, bool? isActive = null, string? nameFilter = null)
        {
            try
            {
                var hotels = await _hotelService.GetAllAsync(shopId, isActive, nameFilter);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // GET: api/hotel/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            try
            {
                var hotel = await _hotelService.GetByIdAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        // POST: api/hotel
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTO hotelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _hotelService.CreateAsync(hotelDto);
                if (!result)
                {
                    return BadRequest("Unable to create hotel.");
                }
                return Ok("Created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating hotel: {ex.Message}");
            }
        }

        // PUT: api/hotel/{id}
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelDTO hotelDto, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _hotelService.UpdateAsync(hotelDto, id);
                if (!result)
                {
                    return BadRequest("Unable to update hotel.");
                }
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating hotel: {ex.Message}");
            }
        }

        // DELETE: api/hotel/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            try
            {
                var result = await _hotelService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok("Delete Sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting hotel: {ex.Message}");
            }
        }
        [HttpGet("{shopId}/report")]
        //[Authorize]
        public async Task<IActionResult> GetShopReport([FromRoute]Guid shopId)
        {
            try
            {
                var result = await _hotelService.GetHotelReport(shopId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }
    }
}
