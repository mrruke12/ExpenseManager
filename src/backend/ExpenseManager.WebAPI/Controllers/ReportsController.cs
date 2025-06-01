using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/report")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase {
        private readonly IAppUserRepository _userRepository;
        private readonly IBankTransferRepository _bankTransferRepository;

        public ReportsController(IAppUserRepository userRepository, IBankTransferRepository bankTransferRepository) {
            _userRepository = userRepository;
            _bankTransferRepository = bankTransferRepository;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserData() {
            var user = await _userRepository.GetByClaimsWithAttachmentsAsync(User);

            return user != null ?
                Ok(user) :
                NotFound();
        }

        [HttpPost("dashboard")]
        public async Task<IActionResult> GetDashboard(DateBoundaries dto) {
            var user = await _userRepository.GetByClaimsAsync(User);
            return Ok(await _bankTransferRepository.GetTransfersForPeriodForUserAsync(user, dto.From, dto.To));
        }

        public record DateBoundaries(DateOnly From, DateOnly To);
    }
}
