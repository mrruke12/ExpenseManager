using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseManager.Core.Services {
    public class TokenService : ITokenService {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config) {
            _config = config;
        }

        public string GenerateToken(AppUser user, int expiresIn = 60) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(expiresIn),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? ExtractUserEmailFromToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userEmail = jwtToken.Claims.First(x => x.Type == "Email");
                return userEmail.Value;
            } catch (Exception ex) {
                return null;
            }
        }
    }
}
