using CommunifyLibrary.Models;
using System.Security.Claims;
using static Communify_Backend.Models.TokenModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface ITokenService
    {
        Task<GenerateTokenResponse> GenerateTokenAsync(GenerateTokenRequest request);

        List<Claim> PrepareClaims(string userID, Role role);


    }
}
