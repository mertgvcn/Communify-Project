using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class SetPasswordRequest
{
    [MaxLength(256), Required]
    public string Password { get; set; }
}
