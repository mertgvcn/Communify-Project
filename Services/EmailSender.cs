using LethalCompany_Backend.Exceptions;
using LethalCompany_Backend.Models.MailSenderModel;
using LethalCompany_Backend.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;


namespace LethalCompany_Backend.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(SendEmailRequest request)
    {
        string subject = _configuration["MailService:MailContent:" + Enum.GetName(request.MailType) + ":Subject"]!;
        string body = _configuration["MailService:MailContent:" + Enum.GetName(request.MailType) + ":Body"]!.Replace("{UrlExtension}", request.UrlExtension);

        if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
        {
            throw new EmailNotConfiguredException("Email subject or body is not configured.");
        }

        var mailConfig = new MailConfig()
        {
            Host = _configuration["MailService:SMTP:Host"]!,
            Port = int.Parse(_configuration["MailService:SMTP:Port"]!),
            SenderName = _configuration["MailService:SenderInformation:Main:Name"]!,
            SenderMail = _configuration["MailService:SenderInformation:Main:Mail"]!,
            SenderPassword = _configuration["MailService:SenderInformation:Main:Password"]
        };

        bool mailConfigNullPropertyFound = mailConfig.GetType().GetProperties().Any(a => a.GetValue(mailConfig) == null);
        if (mailConfigNullPropertyFound)
        {
            throw new EmailNotConfiguredException("Mail config is not configured");
        }

        MailboxAddress mailboxAddressFrom = new MailboxAddress(mailConfig.SenderName, mailConfig.SenderMail);
        MailboxAddress mailboxAddressTo = new MailboxAddress("You", request.ReceiverMail);

        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.From.Add(mailboxAddressFrom);
        mimeMessage.To.Add(mailboxAddressTo);

        mimeMessage.Subject = subject;

        BodyBuilder bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = body;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        SmtpClient client = new SmtpClient();
        client.Connect(mailConfig.Host, mailConfig.Port, false);
        client.Authenticate(mailConfig.SenderMail, mailConfig.SenderPassword);
        client.Send(mimeMessage);
        client.Disconnect(true);
    }
}

