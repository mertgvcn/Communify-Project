namespace CommunifyLibrary.Models;
public class PasswordToken : BaseEntity
{
    public string Token { get; set; }

    public DateTime ExpireDate { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
