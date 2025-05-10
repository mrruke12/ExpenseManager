using ExpenseManager.Core.Interfaces;
using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAppUserRepository _userRepository;
        private readonly IAppUserBuilder _userBuilder;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(IAppUserRepository userRepository, IAppUserBuilder userBuilder, ITokenService tokenService, IConfiguration configuration) {
            _userRepository = userRepository;
            _userBuilder = userBuilder;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto) {
            var user = await _userRepository.ValidateLoginAsync(dto.Email, dto.Password);

            if (user == null) return Unauthorized();

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto) {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);

            if (existing != null) return Conflict();
            if (dto.Password != dto.Verification) return BadRequest();

            var user = _userBuilder.Reset()
                       .SetEmail(dto.Email)
                       .SetPassword(dto.Password)
                       .Build();

            user = await _userRepository.AddAsync(user);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("telegram")]
        public async Task<IActionResult> LoginTelegram() {
            var user = await _userRepository.GetByClaimsAsync(User);

            return user == null ?
                NotFound() :
                Ok(new { LoginLink = $"{_configuration["ConnectionStrings:TelegramBotConnection"]}?start={String.Join("", user.Id.Reverse())}" });
        }

        public record RegisterDto(string Email, string Password, string Verification);
        public record LoginDto(string Email, string Password);
    }
}
