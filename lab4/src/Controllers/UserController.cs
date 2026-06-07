using ComfortSpace.Dto;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComfortSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _userRepository.GetUser(id);
            return Ok(user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest("User data is null");

            if (id != updatedUser.UserId)
                return BadRequest("ID mismatch");

            if (!_userRepository.UserExists(id))
                return NotFound();

            var userFromDb = _userRepository.GetUser(id);

            // Оновлюємо поля відповідно до нової моделі
            userFromDb.Surname = updatedUser.Surname;
            userFromDb.Name = updatedUser.Name;
            userFromDb.Patronymic = updatedUser.Patronymic;
            userFromDb.Email = updatedUser.Email;
            userFromDb.PhoneNumber = updatedUser.PhoneNumber;
            userFromDb.Status = updatedUser.Status;
            userFromDb.Role = updatedUser.Role;

            if (!_userRepository.UpdateUser(userFromDb))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent(); // 204
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var userToDelete = _userRepository.GetUser(id);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
                return StatusCode(500, ModelState);
            }

            return NoContent(); // 204
        }
    }
}