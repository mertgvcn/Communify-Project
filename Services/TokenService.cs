using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Communify_Backend.Models.TokenParameterModels;

namespace Communify_Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));

            var expireDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(500));
            var claims = PrepareClaims(request.UserID, request.Role);

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: configuration["AppSettings:ValidIssuer"],
                    audience: configuration["AppSettings:ValidAudience"],
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expireDate,
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return Task.FromResult(new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = expireDate
            });
        }

        public List<Claim> PrepareClaims(string userID, Role role)
        {
            //Always need ID
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userID),
                new Claim(ClaimTypes.Role, role.Name),
                new Claim("role", role.Name)
            };

            return claims;
        }
    }
}
