using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.TokenModels;

public class GenerateTokenResponse
{
    [MaxLength(256), Required]
    public string Token { get; set; }

    [Required]
    public DateTime TokenExpireDate { get; set; }
}
