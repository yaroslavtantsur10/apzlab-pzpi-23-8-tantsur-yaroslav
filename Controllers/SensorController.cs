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
    public class SensorController : ControllerBase
    {
        private readonly ISensorRepository _sensorRepo;

        public SensorController(ISensorRepository sensorRepo)
        {
            _sensorRepo = sensorRepo;
        }

        // GET: api/sensor
        [HttpGet]
        public IActionResult GetSensors()
        {
            var sensors = _sensorRepo.GetSensors();
            return Ok(sensors);
        }

        //// GET: api/sensor/room/5
        //[HttpGet("room/{roomId}")]
        //public IActionResult GetSensorsByRoom(int roomId)
        //{
        //    var sensors = _sensorRepo.GetSensorsByRoom(roomId);
        //    return Ok(sensors);
        //}

        // GET: api/sensor/5
        [HttpGet("{id}")]
        public IActionResult GetSensor(int id)
        {
            if (!_sensorRepo.SensorExists(id))
                return NotFound();

            var sensor = _sensorRepo.GetSensor(id);
            return Ok(sensor);
        }

        // POST: api/sensor
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateSensor([FromBody] SensorDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Code))
                return BadRequest("Name and Code are required.");

            var sensor = new Sensor
            {
                RoomId = dto.RoomId,
                Name = dto.Name,
                Model = dto.Model,
                Code = dto.Code,
                Unit = dto.Unit,
                Status = dto.Status,
                LastSeenAt = null
            };

            if (!_sensorRepo.CreateSensor(sensor))
                return StatusCode(500, "Failed to create sensor.");

            return Ok(sensor);
        }

        // PUT: api/sensor/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult UpdateSensor(int id, [FromBody] SensorDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (id != dto.SensorId)
                return BadRequest("ID mismatch");

            if (!_sensorRepo.SensorExists(id))
                return NotFound();

            var sensor = _sensorRepo.GetSensor(id);

            sensor.RoomId = dto.RoomId;
            sensor.Name = dto.Name;
            sensor.Model = dto.Model;
            sensor.Code = dto.Code;
            sensor.Unit = dto.Unit;
            sensor.Status = dto.Status;

            if (!_sensorRepo.UpdateSensor(sensor))
                return StatusCode(500, "Failed to update sensor.");

            return NoContent(); // 204
        }

        // DELETE: api/sensor/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteSensor(int id)
        {
            if (!_sensorRepo.SensorExists(id))
                return NotFound();

            var sensor = _sensorRepo.GetSensor(id);

            if (!_sensorRepo.DeleteSensor(sensor))
                return StatusCode(500, "Failed to delete sensor.");

            return NoContent(); // 204
        }
    }
}
