
using LethalCompany_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> EmailExistsAsync(EmailExistsRequest request);
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
        Task RegisterUserAsync(UserRegisterRequest user);
        Task ForgotPasswordAsync(ForgotPasswordRequest request);
        Task SetPasswordAsync(SetPasswordRequest request);
    }
}
