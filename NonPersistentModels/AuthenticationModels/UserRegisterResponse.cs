namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserRegisterResponse
{
    public bool isSuccess { get; set; }

    public string Token { get; set; }

    public DateTime TokenExpireDate { get; set; }
}
