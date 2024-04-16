using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserLoginResponse
{
    public bool AuthenticateResult { get; set; }

    [MaxLength(256)]
    public string AuthToken { get; set; }

    public DateTime AccessTokenExpireDate { get; set; }

    [MaxLength(256)]
    public string ReplyMessage { get; set; }

    [MaxLength(32)]
    public string Role { get; set; }
}
