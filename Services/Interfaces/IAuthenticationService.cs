using static Communify_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> isEmailAvailableAsync(isEmailAvailableRequest request);

        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);

        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest user);

        Task SetPasswordAsync(SetPasswordRequest request);
    }
}
