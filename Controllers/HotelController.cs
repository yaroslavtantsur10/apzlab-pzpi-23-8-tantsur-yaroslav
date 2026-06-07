using ComfortSpace.Dto;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ComfortSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;

        public HotelController(IHotelRepository hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        // GET: api/hotel
        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _hotelRepo.GetHotels();
            return Ok(hotels);
        }

        // GET: api/hotel/{id}
        [HttpGet("{id}")]
        public IActionResult GetHotel(int id)
        {
            if (!_hotelRepo.HotelExists(id))
                return NotFound();

            var hotel = _hotelRepo.GetHotel(id);
            return Ok(hotel);
        }

        // POST: api/hotel
        // Only Admin/Manager
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateHotel([FromBody] HotelDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Hotel name is required.");

            var hotel = new Hotel
            {
                Name = dto.Name,
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                BuildingNumber = dto.BuildingNumber
            };

            if (!_hotelRepo.CreateHotel(hotel))
                return StatusCode(500, "Failed to create hotel.");

            return Ok(hotel);
        }

        // PUT: api/hotel/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult UpdateHotel(int id, [FromBody] HotelDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (id != dto.HotelId)
                return BadRequest("ID mismatch");

            if (!_hotelRepo.HotelExists(id))
                return NotFound();

            var hotel = _hotelRepo.GetHotel(id);

            hotel.Name = dto.Name;
            hotel.Country = dto.Country;
            hotel.City = dto.City;
            hotel.Street = dto.Street;
            hotel.BuildingNumber = dto.BuildingNumber;

            if (!_hotelRepo.UpdateHotel(hotel))
                return StatusCode(500, "Failed to update hotel.");

            return NoContent(); // 204
        }

        // DELETE: api/hotel/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteHotel(int id)
        {
            if (!_hotelRepo.HotelExists(id))
                return NotFound();

            var hotel = _hotelRepo.GetHotel(id);

            if (!_hotelRepo.DeleteHotel(hotel))
                return StatusCode(500, "Failed to delete hotel.");

            return NoContent(); // 204
        }
    }
}