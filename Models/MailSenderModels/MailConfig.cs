using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.MailSenderModel;

public class MailConfig
{
    [MaxLength(32)]
    public string Host { get; set; }

    [MaxLength(4)]
    public int Port { get; set; }

    [MaxLength(64)]
    public string SenderName { get; set; }

    [MaxLength(64)]
    public string SenderMail { get; set; }

    [MaxLength(64)]
    public string SenderPassword { get; set; }
}
