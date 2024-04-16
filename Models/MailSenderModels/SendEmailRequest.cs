using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.MailSenderModel;

public enum MailType
{
    SetPasswordMail,
    ForgotPasswordMail
}

public class SendEmailRequest
{
    [MaxLength(64), Required]
    public string ReceiverMail { get; set; }

    [Required]
    public MailType MailType { get; set; }
}
