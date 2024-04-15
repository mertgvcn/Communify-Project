using LethalCompany_Backend.Models.MailSenderModel;
using LethalCompany_Backend.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;


namespace LethalCompany_Backend.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(SendEmailRequest request)
        {
            MimeMessage mimeMessage = new MimeMessage();
            var mailConfig = new MailConfig()
            {
                Host = _configuration["MailService:SMTP:Host"]!,
                Port = int.Parse(_configuration["MailService:SMTP:Port"]!),
                SenderName = _configuration["MailService:SenderInformation:Main:Name"]!,
                SenderMail = _configuration["MailService:SenderInformation:Main:Mail"]!,
                SenderPassword = _configuration["MailService:SenderInformation:Main:Password"]!
            };
            var mailContent = new MailContent
            {
                Subject = "",
                Body = ""
            };

            if (request.MailType == MailType.SetPasswordMail)
            {
                mailContent.Subject = _configuration["MailService:MailContent:SetPasswordMail:Subject"]!;
                mailContent.Body = _configuration["MailService:MailContent:SetPasswordMail:Body"]!;
            }
            else if (request.MailType == MailType.ForgotPasswordMail)
            {
                mailContent.Subject = _configuration["MailService:MailContent:ForgotPasswordMail:Subject"]!;
                mailContent.Body = _configuration["MailService:MailContent:ForgotPasswordMail:Body"]!;
            }

            MailboxAddress mailboxAddressFrom = new MailboxAddress(mailConfig.SenderName, mailConfig.SenderMail);
            MailboxAddress mailboxAddressTo = new MailboxAddress("You", request.ReceiverMail);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = mailContent.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailContent.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(mailConfig.Host, mailConfig.Port, false);
            client.Authenticate(mailConfig.SenderMail, mailConfig.SenderPassword);
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
