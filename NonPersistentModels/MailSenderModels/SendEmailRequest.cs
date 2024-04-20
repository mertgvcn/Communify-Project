namespace LethalCompany_Backend.Models.MailSenderModel;

public enum MailType
{
    SetPasswordMail,
    ForgotPasswordMail
}

public class SendEmailRequest
{
    public string ReceiverMail { get; set; }

    public MailType MailType { get; set; }
}
