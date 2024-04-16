using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.MailSenderModel;

public class MailConfig
{
    [MaxLength(32), Required]
    public string Host { get; set; }

    [MaxLength(4), Required]
    public int Port { get; set; }

    [MaxLength(64), Required]
    public string SenderName { get; set; }

    [MaxLength(64), Required]
    public string SenderMail { get; set; }

    [MaxLength(64), Required]
    public string SenderPassword { get; set; }
}
