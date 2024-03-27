using static Communify_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);

        Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest user);
    }
}
