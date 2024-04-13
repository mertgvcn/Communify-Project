using static Communify_Backend.Models.AuthenticationModels;
using static Communify_Backend.Models.TokenModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> isEmailAvailableAsync(isEmailAvailableRequest request);

        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);

        Task<GenerateTokenResponse> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest user);

        Task SetPasswordAsync(SetPasswordRequest request);
    }
}
