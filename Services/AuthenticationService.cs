using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.Models;
using CommunifyLibrary.Repository;
using LethalCompany_Backend.Models;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Communify_Backend.Models.AuthenticationModels;
using static Communify_Backend.Models.TokenModels;

namespace Communify_Backend.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextService _httpContextService;

    public AuthenticationService(
        IUserRepository userRepository,
        IInterestRepository interestRepository,
        ITokenService tokenService,
        IEmailSender emailSender,
        IHttpContextService httpContextService
        )
    {
        _userRepository = userRepository;
        _interestRepository = interestRepository;
        _tokenService = tokenService;
        _emailSender = emailSender;
        _httpContextService = httpContextService;
    }

    public async Task<long> GetIdByEmailAsync(string email) => (await _userRepository.GetByEmail(email).SingleAsync()).Id;

    public async Task<bool> isEmailAvailableAsync(isEmailAvailableRequest request) => !await _userRepository.GetByEmail(request.Email).AnyAsync(); //thanks to .Any(), if it finds a email it will return true, otherwise false

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

        var user = _userRepository.GetByEmail(request.Email).Include(user => user.Role).FirstOrDefault();

        if (user is not null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            //Login success
            var generatedToken = await _tokenService.GenerateToken(new GenerateTokenRequest
            {
                UserID = user.Id.ToString(),
                Role = user.Role,
                ExpireDate = DateTime.UtcNow.Add(TimeSpan.FromHours(6))
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

    public async Task<GenerateTokenResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        //Get and update user information to enable forgot password
        var user = await _userRepository.GetByEmail(request.Email).SingleAsync();
        user.Password = null;
        user.RoleId = 3;
        await _userRepository.UpdateAsync(user);

        //Get updated user
        user = await _userRepository.GetByIdAsync(user.Id);

        var generatedToken = _tokenService.GenerateToken(new GenerateTokenRequest
        {
            UserID = user.Id.ToString(),
            ExpireDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            Role = user.Role
        });

        await _emailSender.SendEmailAsync(new SendEmailRequest
        {
            ReceiverMail = request.Email,
            MailType = MailType.ForgotPasswordMail
        });

        return await generatedToken;
    }

    public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)
    {
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
            RoleId = 3,
        };

        var user = await _userRepository.AddAsync(newUser);
        user = await _userRepository.GetAll().Where(u => u.Id == user.Id).Include(u => u.Role).SingleAsync(); //aşağıda role kullandığım için roleid ile eşlemiyordu.


        foreach (var interestId in request.InterestIdList)
        {
            var interest = await _interestRepository.GetByIdAsync(interestId);
            await _userRepository.AddInterest(user.Id, interest);
        }

        //Create token for password
        var generatedToken = await _tokenService.GenerateToken(new GenerateTokenRequest
        {
            UserID = user.Id.ToString(),
            Role = user.Role,
            ExpireDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(5))
        });

        await _emailSender.SendEmailAsync(new SendEmailRequest
        {
            ReceiverMail = newUser.Email,
            MailType = MailType.SetPasswordMail
        });

        return new UserRegisterResponse()
        {
            isSuccess = true,
            Token = generatedToken.Token,
            TokenExpireDate = generatedToken.TokenExpireDate
        };
    }

    public async Task SetPasswordAsync(SetPasswordRequest request)
    {
        var user = await _userRepository.GetAll().Where(a => a.Id == _httpContextService.GetCurrentUserID()).SingleAsync();

        //Hashing the password for security
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        user.RoleId = 2;
        user.Password = hashedPassword;

        await _userRepository.UpdateAsync(user);
    }
}

