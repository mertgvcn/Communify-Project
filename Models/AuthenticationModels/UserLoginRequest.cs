using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserLoginRequest
{
    [MaxLength(64)]
    public string Email { get; set; }

    [MaxLength(256)]
    public string Password { get; set; }
}
