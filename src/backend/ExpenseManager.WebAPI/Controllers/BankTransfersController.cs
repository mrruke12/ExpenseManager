using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using ExpenseManager.Core.Services;
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
        private readonly IWebHostEnvironment _environment;
        private readonly IBankTransferCategoryBuilder _bankTransferCategoryBuilder;

        public BankTransfersController(IAppUserRepository userRepository, IBankTransferRepository bankTransferRepository, IBankTransferBuilder bankTransferBuilder, IBankAccountRepository bankAccountRepository, IBankTransferCategoryRepository bankTransferCategoryRepository, IWebHostEnvironment environment, IBankTransferCategoryBuilder bankTransferCategoryBuilder) {
            _userRepository = userRepository;
            _bankTransferRepository = bankTransferRepository;
            _bankTransferBuilder = bankTransferBuilder;
            _bankAccountRepository = bankAccountRepository;
            _bankTransferCategoryRepository = bankTransferCategoryRepository;
            _environment = environment;
            _bankTransferCategoryBuilder = bankTransferCategoryBuilder;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransfers([FromQuery] BankTransferFilter filter) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var transfers = await _bankTransferRepository.GetTransfersForUser(user, filter);
            return Ok(transfers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer(BankTransferDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);

            var account = await _bankAccountRepository.GetByIdForUserAsync(user, dto.AccountId);
            if (account == null) return BadRequest();

            var transfer = _bankTransferBuilder
                           .Reset()
                           .SetAccountId(dto.AccountId)
                           .SetMoment(dto.Moment)
                           .SetCategoryId(dto.CategoryId)
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

            if (transfer == null) return NotFound();

            await ApplyTransfer(user, transfer);
            return Ok(transfer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransferForUser(int id, BankTransferDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var original = await _bankTransferRepository.GetByIdForUser(user, id);

            if (original == null) return BadRequest();

            var transfer = _bankTransferBuilder
                           .Reference(original)
                           .SetMoment(dto.Moment)
                           .SetCategoryId(dto.CategoryId)
                           .SetBill(dto.Bill)
                           .SetDescription(dto.Description)
                           .Build();

            if (await _bankTransferRepository.UpdateForUserAsync(user, transfer) != true) return BadRequest();

            await CancelTransfer(user, original);
            await ApplyTransfer(user, transfer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var transfer = await _bankTransferRepository.GetByIdForUser(user, id);

            if (transfer == null) return BadRequest();
            if (await _bankTransferRepository.DeleteForUser(user, id) != true) return BadRequest();

            await CancelTransfer(user, transfer);
            return Ok();
        }

        [RequestSizeLimit(1_000_000)]
        [HttpPost("pdf/{accountId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPdf(int accountId, [FromForm] IFormFile pdfFile) {
            if (pdfFile == null || pdfFile.Length == 0) {
                return BadRequest("No file uploaded");
            }

            // Проверка расширения файла
            var extension = Path.GetExtension(pdfFile.FileName).ToLowerInvariant();
            if (extension != ".pdf") {
                return BadRequest("Only PDF files are allowed");
            }

            try {
                // Сохранение файла на сервере
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolder)) {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + pdfFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                    await pdfFile.CopyToAsync(fileStream);
                }

                var user = await _userRepository.GetByClaimsAsync(User);
                var category = await _bankTransferCategoryRepository.GetByNameForUserAsync(user, "kaspi");
                if (category == null) category = await _bankTransferCategoryRepository.AddAsync(_bankTransferCategoryBuilder
                    .Reset()
                    .SetUserId(user.Id)
                    .SetName("kaspi")
                    .Build());

                var res = new PdfService().ImportFromKaspiRu(filePath, accountId, category.Id, _bankTransferBuilder);
                foreach (var t in res) {
                    await _bankTransferRepository.AddAsync(t);
                }

                // Удаление файла после обработки
                if (System.IO.File.Exists(filePath)) {
                    System.IO.File.Delete(filePath);
                }

                return Ok(res);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex}");
            }
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

        public record BankTransferDto(int AccountId, DateTime Moment, int CategoryId, int Bill, string Description);
    }
}