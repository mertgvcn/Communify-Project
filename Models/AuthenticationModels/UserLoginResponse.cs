using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserLoginResponse
{
    [Required]
    public bool AuthenticateResult { get; set; }

    [MaxLength(256), Required]
    public string AuthToken { get; set; }

    [Required]
    public DateTime AccessTokenExpireDate { get; set; }

    [MaxLength(256)]
    public string ReplyMessage { get; set; }

    [MaxLength(32), Required]
    public string Role { get; set; }
}
