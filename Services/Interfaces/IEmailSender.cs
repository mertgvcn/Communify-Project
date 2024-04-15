using LethalCompany_Backend.Models;

namespace LethalCompany_Backend.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(SendEmailRequest request);
    }
}
