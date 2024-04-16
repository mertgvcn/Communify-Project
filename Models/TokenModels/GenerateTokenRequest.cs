using CommunifyLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.TokenModels;

public class GenerateTokenRequest
{
    [MaxLength(32)]
    public string UserID { get; set; }

    public Role Role { get; set; }

    public DateTime ExpireDate { get; set; }
}
