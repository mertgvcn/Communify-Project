namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserLoginRequest
{
    public string Credential { get; set; }

    public string Password { get; set; }
}
