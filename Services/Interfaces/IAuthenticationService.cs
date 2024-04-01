using static Communify_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> isEmailAvailable(isEmailAvailableRequest request);

        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);

        Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest user);

        Task SetPassword(SetPasswordRequest request);
    }
}
