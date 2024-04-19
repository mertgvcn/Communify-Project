namespace LethalCompany_Backend.Models.AuthenticationModels;

public class ForgotPasswordResponse
{
    public bool isSuccess { get; set; }

    public string? Token { get; set; }

    public DateTime? TokenExpireDate { get; set; }
}
