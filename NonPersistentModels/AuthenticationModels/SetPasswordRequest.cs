using CommunifyLibrary.Models;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class SetPasswordRequest
{
    public PasswordToken PasswordToken { get; set; }

    public string Password { get; set; }
}
