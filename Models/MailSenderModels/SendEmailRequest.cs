using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.MailSenderModel;

public enum MailType
{
    SetPasswordMail,
    ForgotPasswordMail
}

public class SendEmailRequest
{
    [MaxLength(64)]
    public string ReceiverMail { get; set; }

    public MailType MailType { get; set; }
}
