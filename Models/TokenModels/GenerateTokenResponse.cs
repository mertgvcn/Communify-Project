using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.TokenModels;

public class GenerateTokenResponse
{
    [MaxLength(256)]
    public string Token { get; set; }

    public DateTime TokenExpireDate { get; set; }
}
