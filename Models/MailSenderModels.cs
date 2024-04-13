namespace LethalCompany_Backend.Models;

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

public class MailConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string SenderName { get; set; }
    public string SenderMail { get; set; }
    public string SenderPassword { get; set; }
}

public class MailContent
{
    public string Subject { get; set; }
    public string Body { get; set; }
}
