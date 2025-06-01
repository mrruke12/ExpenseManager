using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class BankAccountsController : ControllerBase {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankAccountBuilder _bankAccountBuilder;
        private readonly IAppUserRepository _userRepository;

        public BankAccountsController(IBankAccountRepository bankAccountRepository, IBankAccountBuilder bankAccountBuilder, IAppUserRepository userRepository) {
            _bankAccountRepository = bankAccountRepository;
            _bankAccountBuilder = bankAccountBuilder;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAccounts() {
            var user = await _userRepository.GetByClaimsAsync(User);
            var accounts = await _bankAccountRepository.GetByUserAsync(user);

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var account = await _bankAccountRepository.GetByIdForUserAsync(user, id);

            return account == null ?
                NotFound() :
                Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(BankAccountCreateDto dto) {
            if (string.IsNullOrEmpty(dto.name)) return BadRequest();

            var user = await _userRepository.GetByClaimsAsync(User);
            var existing = await _bankAccountRepository.GetByNameForUserAsync(user, dto.name);

            if (existing != null) return Conflict();

            var account = _bankAccountBuilder
                          .Reset()
                          .SetName(dto.name)
                          .SetUserId(user.Id)
                          .Build();

            await _bankAccountRepository.AddAsync(account);

            return Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, BankAccountUpdateDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var original = await _bankAccountRepository.GetByIdForUserAsync(user, id);

            if (original == null) return NotFound();

            var account = _bankAccountBuilder
                          .Reference(original)
                          .SetName(dto.name)
                          .Build();

            return await _bankAccountRepository.UpdateForUserAsync(user, account) == true ?
                Ok(account) :
                NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);

            return await _bankAccountRepository.DeleteForUserAsync(user, id) == true ?
                Ok(user) :
                BadRequest();
        }

        [HttpGet("{id}/transfers")]
        public async Task<IActionResult> GetAccountExpenses(int id, [FromQuery] BankTransferFilter filter) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var expenses = await _bankAccountRepository.GetAccountTransfersAsync(id, user, filter);

            return expenses != null ?
                Ok(expenses) :
                BadRequest();
        }

        public record BankAccountCreateDto(string name);
        public record BankAccountUpdateDto(string name);
    }
}
