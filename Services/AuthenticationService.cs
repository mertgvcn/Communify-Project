using Communify_Backend.Services.Interfaces;
using CommunifyLibrary;
using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using static Communify_Backend.Models.AuthenticationModels;
using static Communify_Backend.Models.TokenParameterModels;

namespace Communify_Backend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CommunifyContext context;
        private readonly ITokenService tokenService;

        public AuthenticationService(CommunifyContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        public async Task<long> GetIdByEmail(string email)
        {
            var user = context.Users.Where(user => user.Email == email).FirstOrDefault();

            if (user is null) return -1;

            return user.Id;
        }

        public async Task<bool> IsEmailAvailable(string email)
        {
            var user = context.Users.Where(user => user.Email == email).FirstOrDefault();

            if (user is null) return true;

            return false;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse()
            {
                AuthenticateResult = false,
                AuthToken = "No Token",
                AccessTokenExpireDate = DateTime.Now,
                Role = "No Role",
                ErrorMessage = ""
            };

            bool isLogin = false;

            var user = context.Users.Where(user => user.Email == request.Email)
                            .Include(user => user.Role).FirstOrDefault();

            //Invalid email
            if (user is null)
            {
                response.ErrorMessage = "Wrong Email";

                return response;
            };

            //Check password is correct
            isLogin = (user.Password == request.Password);

            //Invalid password
            if (!isLogin)
            {
                response.ErrorMessage = "Wrong Password";

                return response;
            }

            //Login success
            var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequest
            {
                UserID = user.Id.ToString(),
                Role = user.Role,
            });

            response.AuthenticateResult = true;
            response.AuthToken = generatedToken.Token;
            response.AccessTokenExpireDate = generatedToken.TokenExpireDate;
            response.Role = user.Role.Name;

            return await Task.FromResult(response);
        }

        public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)
        {
            UserRegisterResponse response = new();
            response.isSuccess = true;

            //Check if email already exists
            if (!await IsEmailAvailable(request.Email))
            {
                response.isSuccess = false;
                response.ErrorMessage = "Email is used by another user.";
                return response;
            }

            //Assign "User" role as default
            var role = context.Roles.Where(x => x.Id == 2).FirstOrDefault();

            User newUser = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = DateTime.Now, //Frontendten gelen doğum tarihinin girilmesi lazım
                BirthCountry = request.PhoneNumber,
                BirthCity = request.Email,
                CurrentCountry = request.CurrentCountry,
                CurrentCity = request.CurrentCity,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password,
                Role = role,
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return await Task.FromResult(response);
        }
    }
}
