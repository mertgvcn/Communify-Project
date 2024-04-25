using CommunifyLibrary.Models;

namespace LethalCompany_Backend.Models.TokenModels;

public class GenerateTokenRequest
{
    public string UserID { get; set; }

    public Role Role { get; set; }

    public DateTime ExpireDate { get; set; }
}
