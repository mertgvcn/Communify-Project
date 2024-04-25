using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.Models;
using CommunifyLibrary.Repository;
using CommunifyLibrary.Repository.Interfaces;
using LethalCompany_Backend.Exceptions;
using LethalCompany_Backend.Models.TokenModels;
using LethalCompany_Backend.NonPersistentModels.TokenModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Communify_Backend.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IPasswordTokenRepository _passwordTokenRepository;
    private readonly IUserRepository _userRepository;

    public TokenService(IConfiguration configuration, IPasswordTokenRepository passwordTokenRepository, IUserRepository userRepository)
    {
        _configuration = configuration;
        _passwordTokenRepository = passwordTokenRepository;
        _userRepository = userRepository;
    }

    public Task<GenerateTokenResponse> GenerateTokenAsync(GenerateTokenRequest request)
    {
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));

        List<Claim> claims = PrepareClaims(request.UserID, request.Role, request.ExpireDate);

        JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["AppSettings:ValidIssuer"],
                audience: _configuration["AppSettings:ValidAudience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: request.ExpireDate,
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)

            );

        return Task.FromResult(new GenerateTokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            ExpireDate = request.ExpireDate
        });
    }

    public List<Claim> PrepareClaims(string userID, Role role, DateTime expireDate)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userID),
            new Claim(ClaimTypes.Role, role.Name),
            new Claim("role", role.Name),
            new Claim("expireDate", expireDate.ToString())
        };

        return claims;
    }

    public async Task<GenerateTokenResponse> CreatePasswordTokenAsync(long userId)
    {
        var role = await _userRepository.GetAll().Where(u => u.Id == userId).Include(u => u.Role).Select(u => u.Role).SingleAsync();

        var token = await GenerateTokenAsync(new GenerateTokenRequest
        {
            UserID = userId.ToString(),
            ExpireDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            Role = role,
        });

        await _passwordTokenRepository.AddAsync(new PasswordToken
        {
            Token = token.Token,
            ExpireDate = token.ExpireDate,
            UserId = userId,
        });

        return new GenerateTokenResponse
        {
            Token = token.Token,
            ExpireDate = token.ExpireDate,
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
