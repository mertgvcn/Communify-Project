using Communify_Backend.Services.Interfaces;
using CommunifyLibrary;
using CommunifyLibrary.Models;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Communify_Backend.Models.AuthenticationModels;
using static Communify_Backend.Models.TokenParameterModels;

namespace Communify_Backend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CommunifyContext context;
        private readonly ITokenService tokenService;
        private readonly IEmailSender emailSender;

        public AuthenticationService(CommunifyContext context, ITokenService tokenService, IEmailSender emailSender)
        {
            this.context = context;
            this.tokenService = tokenService;
            this.emailSender = emailSender;
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
                ReplyMessage = "Email or password is wrong",
                Role = "No Role",
            };

            var user = context.Users.Where(user => user.Email == request.Email)
                            .Include(user => user.Role).FirstOrDefault();

            if (user is not null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                //Login success
                var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequest
                {
                    UserID = user.Id.ToString(),
                    Role = user.Role,
                });

                response.AuthenticateResult = true;
                response.AuthToken = generatedToken.Token;
                response.AccessTokenExpireDate = generatedToken.TokenExpireDate;
                response.ReplyMessage = "Login Successful";
                response.Role = user.Role.Name;

                return await Task.FromResult(response);
            }

            return await Task.FromResult(response);
        }

        public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)
        {
            UserRegisterResponse response = new();
            response.isSuccess = true;
            response.ReplyMessage = "Register successful.";

            //Check if email already exists
            if (!await IsEmailAvailable(request.Email))
            {
                response.isSuccess = false;
                response.ReplyMessage = "Email is used by another user.";
                return response;
            }

            //Assign "User" role as default
            var role = context.Roles.Where(x => x.Id == 2).FirstOrDefault();

            //Hashing the password for security
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User newUser = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                BirthCountry = request.PhoneNumber,
                BirthCity = request.Email,
                CurrentCountry = request.CurrentCountry,
                CurrentCity = request.CurrentCity,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = hashedPassword, //email sistemi gelince kalkacak
                Role = role,
                //interestleri eklicez
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return await Task.FromResult(response);
        }

        public async Task SendEmail(string toEmail)
        {
            await emailSender.SendEmailAsync(toEmail);
        }
    }
}
