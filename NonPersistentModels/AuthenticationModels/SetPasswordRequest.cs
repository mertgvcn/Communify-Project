namespace LethalCompany_Backend.Models.AuthenticationModels;

public class SetPasswordRequest
{
    public long UserId { get; set; }

    public string Password { get; set; }
}
