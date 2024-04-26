using CommunifyLibrary.Models;
using CommunifyLibrary.NonPersistentModels.ParameterModels;
using System.Security.Claims;

namespace Communify_Backend.Services.Interfaces
{
    public interface ITokenService
    {
        Task<GenerateTokenResponse> GenerateTokenAsync(GenerateTokenRequest request);

        List<Claim> PrepareClaims(string userID, Role role, DateTime expireDate);

        Task<GenerateTokenResponse> CreatePasswordTokenAsync(long userId);

        Task<bool> PasswordTokenExistsAsync(PasswordTokenExistsRequest request);
    }
}
