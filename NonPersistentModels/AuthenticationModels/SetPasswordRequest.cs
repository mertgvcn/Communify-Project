namespace LethalCompany_Backend.Models.AuthenticationModels;

public class SetPasswordRequest
{
    public string Token { get; set; }

    public string Password { get; set; }
}
