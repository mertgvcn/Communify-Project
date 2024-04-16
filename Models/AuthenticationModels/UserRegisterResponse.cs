using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserRegisterResponse
{
    [Required]
    public bool isSuccess { get; set; }

    [MaxLength(256), Required]
    public string Token { get; set; }

    [Required]
    public DateTime TokenExpireDate { get; set; }
}
