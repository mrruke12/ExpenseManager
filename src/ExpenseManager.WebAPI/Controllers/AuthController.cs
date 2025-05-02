using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAppUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(IAppUserRepository userRepository, ITokenService tokenService, IConfiguration configuration) {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            var user = await _userRepository.ValidateAsync(request.Email, request.Password);

            if (user == null) return Unauthorized();

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null) {
                Log.Warning($"User with email \"{request.Email}\" already exist");
                return BadRequest("User already exists");
            }

            if (request.Password != request.PasswordVerification) return BadRequest("Passwords do not match");

            var user = new AppUser {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _userRepository.AddAsync(user);

            return Ok("юзер создан");
        }

        [Authorize]
        [HttpGet("LoginTelegram")]
        public async Task<IActionResult> LoginTelegram() {
            var email = User.FindFirst("Email").Value;
            var user = await _userRepository.GetByEmailAsync(email);
            return Ok(new { LoginLink = $"{_configuration["ConnectionStrings:TelegramBotConnection"]}?start={String.Join("", user.Id.Reverse())}" });
        }

        public record RegisterRequest(string Email, string Password, string PasswordVerification);
        public record LoginRequest(string Email, string Password);
    }
}
