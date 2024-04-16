using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class EmailExistsRequest
{
    [MaxLength(64), Required]
    public string Email { get; set; }
}
