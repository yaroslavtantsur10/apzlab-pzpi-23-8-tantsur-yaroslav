using ComfortSpace.Dto;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;
using ComfortSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComfortSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepo, PasswordService passwordService, JwtService jwtService)
        {
            _userRepo = userRepo;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (dto == null)
                return BadRequest();

            var email = dto.Email?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            if (_userRepo.EmailExists(email))
                return Conflict("Email already exists.");

            var user = new User
            {
                Surname = dto.Surname,
                Name = dto.Name,
                Patronymic = dto.Patronymic,
                Email = email,
                PhoneNumber = null,
                Status = "Active",
                Role = "Guest"
            };

            // Хешуємо пароль і кладемо у поле Password
            user.Password = _passwordService.Hash(user, dto.Password);

            if (!_userRepo.CreateUser(user))
                return StatusCode(500, "Failed to create user.");

            return Ok("Registered successfully.");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto == null)
                return BadRequest();

            var email = dto.Email?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            var user = _userRepo.GetByEmail(email);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            if (!_passwordService.Verify(user, user.Password, dto.Password))
                return Unauthorized("Invalid credentials.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                userId = user.UserId,
                user.Surname,
                user.Name,
                user.Email,
                user.Role
            });
        }
    }
}

