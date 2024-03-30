namespace LethalCompany_Backend.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string receiverMail);
    }
}
