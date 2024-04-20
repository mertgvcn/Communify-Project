using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.Models;
using CommunifyLibrary.Repository;
using LethalCompany_Backend.Models.AuthenticationModels;
using LethalCompany_Backend.Models.MailSenderModel;
using LethalCompany_Backend.Models.TokenModels;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Communify_Backend.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly ITokenService _tokenService;
    private readonly ICryptionService _cryptionService;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextService _httpContextService;

    public AuthenticationService(
        IUserRepository userRepository,
        IInterestRepository interestRepository,
        ITokenService tokenService,
        ICryptionService cryptionService,
        IEmailSender emailSender,
        IHttpContextService httpContextService
        )
    {
        _userRepository = userRepository;
        _interestRepository = interestRepository;
        _tokenService = tokenService;
        _cryptionService = cryptionService;
        _emailSender = emailSender;
        _httpContextService = httpContextService;
    }

    public async Task<long> GetIdByEmailAsync(string email) => (await _userRepository.GetByEmail(email).SingleAsync()).Id;

    public async Task<bool> EmailExistsAsync(EmailExistsRequest request) => await _userRepository.GetByEmail(request.Email).AnyAsync(); //thanks to .Any(), if it finds a email it will return true, otherwise false

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
        var plainPassword = await _cryptionService.Decrypt(request.Password);

        if (user is not null && BCrypt.Net.BCrypt.Verify(plainPassword, user.Password))
        {
            var generatedToken = await _tokenService.GenerateTokenAsync(new GenerateTokenRequest
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

    public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var emailExists = await EmailExistsAsync(new EmailExistsRequest() { Email = request.Email });
        if (!emailExists)
            return new ForgotPasswordResponse()
            {
                isSuccess = false,
                Token = null,
                TokenExpireDate = null
            };

        var user = await _userRepository.GetByEmail(request.Email).SingleAsync();
        user.Password = null;
        user.RoleId = 3;
        await _userRepository.UpdateAsync(user);

        user = await _userRepository.GetAll().Where(u => u.Id == user.Id).Include(u => u.Role).SingleAsync();

        var generatedToken = await _tokenService.GenerateTokenAsync(new GenerateTokenRequest
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

        return new ForgotPasswordResponse()
        {
            isSuccess = true,
            Token = generatedToken.Token,
            TokenExpireDate = generatedToken.TokenExpireDate
        };
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
        user = await _userRepository.GetAll().Where(u => u.Id == user.Id).Include(u => u.Role).SingleAsync();


        foreach (var interestId in request.InterestIdList)
        {
            var interest = await _interestRepository.GetByIdAsync(interestId);
            await _userRepository.AddInterest(user.Id, interest);
        }

        var generatedToken = await _tokenService.GenerateTokenAsync(new GenerateTokenRequest
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
        var plainPassword = await _cryptionService.Decrypt(request.Password);

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

        user.RoleId = 2;
        user.Password = hashedPassword;

        await _userRepository.UpdateAsync(user);
    }
}

