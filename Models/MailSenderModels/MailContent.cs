using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.MailSenderModel;

public class MailContent
{
    [MaxLength(64), Required]
    public string Subject { get; set; }

    [MaxLength(255), Required]
    public string Body { get; set; }
}
