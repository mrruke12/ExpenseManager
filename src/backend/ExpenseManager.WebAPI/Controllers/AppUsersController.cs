using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/users/")]
    [ApiController]
    [Authorize]
    public class AppUsersController : ControllerBase {
        private readonly IAppUserRepository _userRepository;
        private readonly IAppUserBuilder _userBuilder;

        public AppUsersController(IAppUserRepository userRepository, IAppUserBuilder userBuilder) {
            _userRepository = userRepository;
            _userBuilder = userBuilder;
        }

        [HttpGet("current")]
        public async Task<IActionResult> CurrentUser() {
            var user = await _userRepository.GetByClaimsAsync(User);

            return user == null ?
                NotFound() :
                Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto) {
            var original = await _userRepository.GetByClaimsAsync(User);
            if (original == null) return NotFound();

            if (dto.newPassword != dto.verification) return BadRequest();

            var user = _userBuilder
                        .Reset(original)
                        .SetPassword(dto.newPassword)
                        .Build();

            await _userRepository.UpdateAsync(user);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser() {
            var user = await _userRepository.GetByClaimsAsync(User);

            if (user == null) return BadRequest();

            await _userRepository.DeleteAsync(user.Id);

            return Ok();
        }

        public record UpdateUserDto(string newPassword, string verification);
    }
}
