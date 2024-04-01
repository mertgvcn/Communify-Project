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
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IEmailSender emailSender;

        public AuthenticationService(CommunifyContext context, IUserService userService, ITokenService tokenService, IEmailSender emailSender)
        {
            this.context = context;
            this.userService = userService;
            this.tokenService = tokenService;
            this.emailSender = emailSender;
        }

        public async Task<long> GetIdByEmail(string email)
        {
            var user = context.Users.Where(user => user.Email == email).FirstOrDefault();

            if (user is null) return -1;

            return user.Id;
        }

        public async Task<bool> isEmailAvailable(isEmailAvailableRequest request)
        {
            var user = context.Users.Where(user => user.Email == request.Email).FirstOrDefault();

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
            //Assign "UnAuthorizedUser" role as default
            var role = context.Roles.Where(x => x.Id == 3).FirstOrDefault();

            User newUser = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate,
                Email = request.Email,
                Gender = request.Gender,
                BirthCountry = request.PhoneNumber,
                BirthCity = request.Email,
                CurrentCountry = request.CurrentCountry,
                CurrentCity = request.CurrentCity,
                Address = request.Address,
                Role = role,
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            var user = context.Users.Where(a => a.Email == newUser.Email).FirstOrDefault();

            //Create user's interest list
            if (user.Interests is null)
            {
                user.Interests = new List<Interest>();
            }

            foreach (var interestId in request.InterestIdList)
            {
                var interest = context.Interests.Where(i => i.Id == interestId).FirstOrDefault();

                user.Interests.Add(interest);
            }

            await context.SaveChangesAsync();

            //Create token for password
            var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequest
            {
                UserID = user.Id.ToString(),
                Role = user.Role,
            });

            await emailSender.SendEmailAsync(newUser.Email);

            return new UserRegisterResponse()
            {
                isSuccess = true,
                Token = generatedToken.Token,
                TokenExpireDate = generatedToken.TokenExpireDate,
            };
        }

        public async Task SetPassword(SetPasswordRequest request)
        {
            var userId = userService.GetCurrentUserID();

            var user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var role = context.Roles.Where(r => r.Id == 2).FirstOrDefault();

            //Hashing the password for security
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Role = role;
            user.Password = hashedPassword;

            context.Attach(user);
            context.Entry(user).Property(p => p.Role).IsModified = true;
            context.Entry(user).Property(p => p.Password).IsModified = true;
            await context.SaveChangesAsync();
        }
    }
}
