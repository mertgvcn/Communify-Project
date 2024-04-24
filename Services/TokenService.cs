using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.Models;
using CommunifyLibrary.Repository.Interfaces;
using LethalCompany_Backend.Exceptions;
using LethalCompany_Backend.Models.TokenModels;
using LethalCompany_Backend.NonPersistentModels.TokenModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Communify_Backend.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IPasswordTokenRepository _passwordTokenRepository;

    public TokenService(IConfiguration configuration, IPasswordTokenRepository passwordTokenRepository)
    {
        _configuration = configuration;
        _passwordTokenRepository = passwordTokenRepository;
    }

    public Task<GenerateTokenResponse> GenerateTokenAsync(GenerateTokenRequest request)
    {
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));

        var expireDate = request.ExpireDate;
        List<Claim>? claims = null;

        if (request.Role is not null)
        {
            claims = PrepareClaims(request.UserID, request.Role);
        }

        JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["AppSettings:ValidIssuer"],
                audience: _configuration["AppSettings:ValidAudience"],
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
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userID),
            new Claim(ClaimTypes.Role, role.Name),
            new Claim("role", role.Name)
        };

        return claims;
    }

    public async Task<GenerateTokenResponse> CreatePasswordTokenAsync(long userId)
    {
        var token = await GenerateTokenAsync(new GenerateTokenRequest
        {
            UserID = userId.ToString(),
            ExpireDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(5))
        });

        await _passwordTokenRepository.AddAsync(new PasswordToken
        {
            Token = token.Token,
            ExpireDate = token.TokenExpireDate,
            UserId = userId,
        });

        return new GenerateTokenResponse
        {
            Token = token.Token,
            TokenExpireDate = token.TokenExpireDate,
        };
    }

    public async Task<bool> PasswordTokenExistsAsync(PasswordTokenExists request)
    {
        var token = await _passwordTokenRepository.GetByTokenAsync(request.Token);

        if (token is not null)
        {
            if (token.ExpireDate != request.ExpireDate)
            {
                //To Do: Add user to blacklist
                throw new TokenManipulationException("PasswordToken expire date manipulated.");
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
