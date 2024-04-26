using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.Models;
using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.Repository;
using CommunifyLibrary.Repository.Interfaces;
using LethalCompany_Backend.Models.MailSenderModel;
using LethalCompany_Backend.Models.TokenModels;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Communify_Backend.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly IPasswordTokenRepository _passwordTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly ICryptionService _cryptionService;
    private readonly IEmailSender _emailSender;

    public AuthenticationService(
        IUserRepository userRepository,
        IInterestRepository interestRepository,
        IPasswordTokenRepository passwordTokenRepository,
        ITokenService tokenService,
        ICryptionService cryptionService,
        IEmailSender emailSender
        )
    {
        _userRepository = userRepository;
        _interestRepository = interestRepository;
        _passwordTokenRepository = passwordTokenRepository;
        _tokenService = tokenService;
        _cryptionService = cryptionService;
        _emailSender = emailSender;
    }

    public async Task<bool> EmailExistsAsync(EmailExistsRequest request) => await _userRepository.GetByEmail(request.Email).AnyAsync();

    public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
    {
        UserLoginResponse response = new UserLoginResponse()
        {
            AuthenticateResult = false,
            AuthToken = "No Token",
            AccessTokenExpireDate = DateTime.Now,
            ReplyMessage = "Your credentials are wrong",
            Role = "No Role",
        };

        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"); //email check regex
        Match isEmail = regex.Match(request.Credential);

        User user;

        if (isEmail.Success)
        {
            user = await _userRepository.GetByEmail(request.Credential).Include(user => user.Role).FirstOrDefaultAsync();
        }
        else
        {
            user = await _userRepository.GetByUsername(request.Credential).Include(user => user.Role).FirstOrDefaultAsync();
        }

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
            response.AccessTokenExpireDate = generatedToken.ExpireDate;
            response.ReplyMessage = "Login Successful";
            response.Role = user.Role.Name;

            return await Task.FromResult(response);
        }

        return await Task.FromResult(response);
    }

    public async Task RegisterUserAsync(UserRegisterRequest request)
    {
        User newUser = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            PhoneNumber = request.PhoneNumber,
            BirthDate = request.BirthDate,
            Email = request.Email,
            Gender = request.Gender,
            BirthCountry = request.PhoneNumber,
            BirthCity = request.Email,
            CurrentCountry = request.CurrentCountry,
            CurrentCity = request.CurrentCity,
            Address = request.Address,
            RoleId = 2,
        };

        var user = await _userRepository.AddAsync(newUser);

        foreach (var interestId in request.InterestIdList)
        {
            var interest = await _interestRepository.GetByIdAsync(interestId);
            await _userRepository.AddInterest(user.Id, interest);
        }

        var generatedToken = await _tokenService.CreatePasswordTokenAsync(user.Id);

        await _emailSender.SendEmailAsync(new SendEmailRequest
        {
            ReceiverMail = newUser.Email,
            MailType = MailType.SetPasswordMail,
            UrlExtension = "setpassword?token=" + generatedToken.Token
        });
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var emailExists = await EmailExistsAsync(new EmailExistsRequest() { Email = request.Email });
        if (!emailExists) return;

        var userId = await _userRepository.GetIdByEmailAsync(request.Email);
        var generatedToken = await _tokenService.CreatePasswordTokenAsync(userId);

        await _emailSender.SendEmailAsync(new SendEmailRequest
        {
            ReceiverMail = request.Email,
            MailType = MailType.ForgotPasswordMail,
            UrlExtension = "setpassword?token=" + generatedToken.Token
        });
    }

    public async Task SetPasswordAsync(SetPasswordRequest request)
    {
        var passwordToken = await _passwordTokenRepository.GetByTokenAsync(request.Token);

        if (passwordToken is null) return;

        if (DateTime.UtcNow < passwordToken.ExpireDate)
        {
            var user = await _userRepository.GetAll().Where(a => a.Id == passwordToken.UserId).SingleAsync();
            var plainPassword = await _cryptionService.Decrypt(request.Password);

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            user.RoleId = 2;
            user.Password = hashedPassword;

            await _userRepository.UpdateAsync(user);
        }

        await _passwordTokenRepository.DeleteAsync(passwordToken);

    }
}

