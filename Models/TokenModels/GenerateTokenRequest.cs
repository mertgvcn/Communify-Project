using CommunifyLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.TokenModels;

public class GenerateTokenRequest
{
    [MaxLength(32), Required]
    public string UserID { get; set; }

    [Required]
    public Role Role { get; set; }

    [Required]
    public DateTime ExpireDate { get; set; }
}
