
using LethalCompany_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> EmailExistsAsync(EmailExistsRequest request);

        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);

        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest user);

        Task SetPasswordAsync(SetPasswordRequest request);
    }
}
