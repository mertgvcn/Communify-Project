namespace LethalCompany_Backend.Models.TokenModels;

public class GenerateTokenResponse
{
    public string Token { get; set; }
    public DateTime TokenExpireDate { get; set; }
}
