using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class SetPasswordRequest
{
    [MaxLength(256)]
    public string Password { get; set; }
}
