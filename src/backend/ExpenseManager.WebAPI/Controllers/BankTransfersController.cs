using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/transfers")]
    [ApiController]
    [Authorize]
    public class BankTransfersController : ControllerBase {
        private readonly IAppUserRepository _userRepository;
        private readonly IBankTransferRepository _bankTransferRepository;
        private readonly IBankTransferBuilder _bankTransferBuilder;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankTransferCategoryRepository _bankTransferCategoryRepository;

        public BankTransfersController(IAppUserRepository userRepository, IBankTransferRepository bankTransferRepository, IBankTransferBuilder bankTransferBuilder, IBankAccountRepository bankAccountRepository, IBankTransferCategoryRepository bankTransferCategoryRepository) {
            _userRepository = userRepository;
            _bankTransferRepository = bankTransferRepository;
            _bankTransferBuilder = bankTransferBuilder;
            _bankAccountRepository = bankAccountRepository;
            _bankTransferCategoryRepository = bankTransferCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransfers([FromQuery] BankTransferFilter filter) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var transfers = await _bankTransferRepository.GetTransfersForUser(user, filter);
            return Ok(transfers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer(CreateBankTransferDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);

            var account = await _bankAccountRepository.GetByIdForUserAsync(user, dto.AccountId);
            var category = await _bankTransferCategoryRepository.GetByIdForUserAsync(user, dto.CategoryId);
            if (category == null || account == null) return BadRequest();

            var transfer = _bankTransferBuilder
                           .Reset()
                           .SetAccountId(dto.AccountId)
                           .SetMoment(dto.Moment)
                           .SetCategory(category)
                           .SetBill(dto.Bill)
                           .SetDescription(dto.Description)
                           .Build();

            transfer = await _bankTransferRepository.AddAsync(transfer);

            return transfer != null ?
                    Ok(transfer) :
                    BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferByIdForUser(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var transfer = await _bankTransferRepository.GetByIdForUser(user, id);

            if (transfer != null) {
                ApplyTransfer(user, transfer);
                return Ok(transfer);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransferForUser(int id, TransferUpdateDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var original = await _bankTransferRepository.GetByIdForUser(user, id);

            var category = await _bankTransferCategoryRepository.GetByIdForUserAsync(user, dto.CategoryId);
            if (category == null) return BadRequest();

            var transfer = _bankTransferBuilder
                           .Reset(original)
                           .SetMoment(dto.Moment)
                           .SetCategory(category)
                           .SetBill(dto.Bill)
                           .SetDescription(dto.Description)
                           .Build();

            if (await _bankTransferRepository.UpdateForUserAsync(user, transfer) == true) {
                CancelTransfer(user, original);
                ApplyTransfer(user, transfer);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var transfer = await _bankTransferRepository.GetByIdForUser(user, id);

            if (await _bankTransferRepository.DeleteForUser(user, id) == true) {
                CancelTransfer(user, transfer);
                return Ok();
            }

            return BadRequest();
        }

        private async Task ApplyTransfer(AppUser user, BankTransfer transfer) {
            var account = await _bankAccountRepository.GetByIdForUserAsync(user, transfer.AccountId);
            account.Balance += transfer.Bill;
            await _bankAccountRepository.UpdateForUserAsync(user, account);
        }

        private async Task CancelTransfer(AppUser user, BankTransfer transfer) {
            var account = await _bankAccountRepository.GetByIdForUserAsync(user, transfer.AccountId);
            account.Balance -= transfer.Bill;
            await _bankAccountRepository.UpdateForUserAsync(user, account);
        }

        public record TransferUpdateDto(DateTime Moment, int CategoryId, int Bill, string Description);
        public record CreateBankTransferDto(int AccountId, DateTime Moment, int CategoryId, int Bill, string Description);
    }
}
