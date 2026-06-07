using ComfortSpace.Dto;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComfortSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepo;

        public RoomController(IRoomRepository roomRepo)
        {
            _roomRepo = roomRepo;
        }

        // GET: api/room
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomRepo.GetRooms();
            return Ok(rooms);
        }

        // GET: api/room/{id}
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            if (!_roomRepo.RoomExists(id))
                return NotFound();

            var room = _roomRepo.GetRoom(id);
            return Ok(room);
        }

        // POST: api/room
        // Only Manager or Admin
        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult CreateRoom([FromBody] RoomDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(dto.RoomNumber))
                return BadRequest("RoomNumber is required.");

            var room = new Room
            {
                HotelId = dto.HotelId,
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                Floor = dto.Floor
            };

            if (!_roomRepo.CreateRoom(room))
                return StatusCode(500, "Failed to create room.");

            return Ok(room);
        }

        // PUT: api/room/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (id != dto.RoomId)
                return BadRequest("ID mismatch");

            if (!_roomRepo.RoomExists(id))
                return NotFound();

            var room = _roomRepo.GetRoom(id);

            room.HotelId = dto.HotelId;
            room.RoomNumber = dto.RoomNumber;
            room.RoomType = dto.RoomType;
            room.Floor = dto.Floor;

            if (!_roomRepo.UpdateRoom(room))
                return StatusCode(500, "Failed to update room.");

            return NoContent();
        }

        // DELETE: api/room/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult DeleteRoom(int id)
        {
            if (!_roomRepo.RoomExists(id))
                return NotFound();

            var room = _roomRepo.GetRoom(id);

            if (!_roomRepo.DeleteRoom(room))
                return StatusCode(500, "Failed to delete room.");

            return NoContent();
        }
    }
}
