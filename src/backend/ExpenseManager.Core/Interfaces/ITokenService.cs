using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces {
    public interface ITokenService {
        string GenerateToken(AppUser user, int expiresIn = 60);
        public string? ExtractUserEmailFromToken(string token);
    }
}
