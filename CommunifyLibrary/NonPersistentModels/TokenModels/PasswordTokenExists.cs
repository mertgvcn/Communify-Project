namespace LethalCompany_Backend.NonPersistentModels.TokenModels;

public class PasswordTokenExists
{
    public string Token { get; set; }

    public DateTime ExpireDate { get; set; }
}
