using System.ComponentModel.DataAnnotations;

namespace CommunifyLibrary.Models;
public class ForgotPassword : BaseEntity
{
    [MaxLength(65)]
    public string Token { get; set; }
    public User User { get; set; }
    public long UserId { get; set; }

}
