using LethalCompany_Backend.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;


namespace LethalCompany_Backend.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string receiverMail)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress(configuration["PasswordMail:SenderName"], configuration["PasswordMail:SenderMail"]);
            MailboxAddress mailboxAddressTo = new MailboxAddress("You", receiverMail);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);


            mimeMessage.Subject = configuration["PasswordMail:Subject"];

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = configuration["PasswordMail:Body"];
            mimeMessage.Body = bodyBuilder.ToMessageBody();


            SmtpClient client = new SmtpClient();
            client.Connect(configuration["SMTP:Host"], int.Parse(configuration["SMTP:Port"]), false);
            client.Authenticate(configuration["PasswordMail:SenderMail"], configuration["PasswordMail:Password"]);
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
