using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserRegisterResponse
{
    public bool isSuccess { get; set; }

    [MaxLength(256)]
    public string Token { get; set; }

    public DateTime TokenExpireDate { get; set; }
}
