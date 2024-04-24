using CommunifyLibrary.Models;
using LethalCompany_Backend.Models.TokenModels;
using LethalCompany_Backend.NonPersistentModels.TokenModels;
using System.Security.Claims;

namespace Communify_Backend.Services.Interfaces
{
    public interface ITokenService
    {
        Task<GenerateTokenResponse> GenerateTokenAsync(GenerateTokenRequest request);

        List<Claim> PrepareClaims(string userID, Role role);

        Task<GenerateTokenResponse> CreatePasswordTokenAsync(long userId);

        Task<bool> PasswordTokenExistsAsync(PasswordTokenExists request);
    }
}
