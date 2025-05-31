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

        [HttpGet("current-locale")]
        public async Task<IActionResult> CurrentLocale() {
            var user = await _userRepository.GetByClaimsAsync(User);

            return user == null ?
                NotFound() :
                Ok(user.LanguageCode);
        }

        [HttpPut("current-locale")]
        public async Task<IActionResult> UpdateUserLocale(ChangeLocaleDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);

            user = _userBuilder
                .Reference(user)
                .SetLanguageCode(dto.Locale)
                .Build();


            await _userRepository.UpdateAsync(user);
            return Ok();
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdateUserPassword(string Password) {
            var original = await _userRepository.GetByClaimsAsync(User);
            if (original == null) return NotFound();


            var user = _userBuilder
                        .Reference(original)
                        .SetPassword(Password)
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

        public record ChangeLocaleDto(string Locale);
    }
}
