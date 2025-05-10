using ExpenseManager.Core.Interfaces.Builders;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class BankTransferCategoriesController : ControllerBase {
        private readonly IAppUserRepository _userRepository;
        private readonly IBankTransferCategoryRepository _bankTransferCategoryRepository;
        private readonly IBankTransferCategoryBuilder _bankTransferCategoryBuilder;

        public BankTransferCategoriesController(IAppUserRepository appUserRepository, IBankTransferCategoryRepository bankTransferCategoryRepository, IBankTransferCategoryBuilder bankTransferCategoryBuilder) {
            _userRepository = appUserRepository;
            _bankTransferCategoryRepository = bankTransferCategoryRepository;
            _bankTransferCategoryBuilder = bankTransferCategoryBuilder;
        }


        [HttpGet]
        public async Task<IActionResult> GetUserCategories() {
            var user = await _userRepository.GetByClaimsAsync(User);

            var categories = await _bankTransferCategoryRepository.GetForUserAsync(user);

            return categories != null ?
                Ok(categories) :
                BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCategory(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);

            var category = await _bankTransferCategoryRepository.GetByIdForUserAsync(user, id);

            return category != null ?
                Ok(category) :
                BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateBankTransferCategoryDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var existing = await _bankTransferCategoryRepository.GetByNameForUserAsync(user, dto.name);

            if (existing != null) return Conflict();

            var category = _bankTransferCategoryBuilder
                            .Reset()
                            .SetName(dto.name)
                            .SetUserId(user.Id)
                            .Build();

            category = await _bankTransferCategoryRepository.AddAsync(category);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateBankTransferCategoryDto dto) {
            var user = await _userRepository.GetByClaimsAsync(User);
            var original = await _bankTransferCategoryRepository.GetByIdForUserAsync(user, id);

            if (original == null) return BadRequest();

            var category = _bankTransferCategoryBuilder
                            .Reset(original)
                            .SetName(dto.name)
                            .Build();

            return await _bankTransferCategoryRepository.UpdateForUserAsync(user, category) ?
                            Ok() :
                            BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) {
            var user = await _userRepository.GetByClaimsAsync(User);
            return await _bankTransferCategoryRepository.DeleteForUserAsync(user, id) ?
                            Ok() :
                            BadRequest();
        }

        public record CreateBankTransferCategoryDto(string name);
        public record UpdateBankTransferCategoryDto(string name);
    }
}
