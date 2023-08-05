using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TaskifyAuthService.Web.Models;
using TaskifyAuthService.Web.Utils;

namespace TaskifyAuthService.Web.Services
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }

    public class JWTManagerRepository : IJWTManagerRepository
    {

        private readonly IConfiguration _configuration;
        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private byte[] JwtKey => _configuration.GetJwtKey();

        public Tokens GenerateRefreshToken(string userName)
        {
            return GenerateJWTTokens(userName);
        }

        public Tokens GenerateToken(string userName)
        {
            return GenerateJWTTokens(userName);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JwtKey),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }

            throw new SecurityTokenException("Invalid token");
        }

        private Tokens GenerateJWTTokens(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var expires = _configuration.AuthTokenExpiration();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                         new Claim(ClaimTypes.Actor, userName),
                         new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetJwtAudience()),
                         new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetJwtIssuer())
                  }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(JwtKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();
            return new Tokens { AccessToken = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
