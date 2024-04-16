using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserLoginRequest
{
    [MaxLength(64), Required]
    public string Email { get; set; }

    [MaxLength(256), Required]
    public string Password { get; set; }
}
