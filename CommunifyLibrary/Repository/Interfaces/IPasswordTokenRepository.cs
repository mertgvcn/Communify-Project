using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository.Interfaces;
public interface IPasswordTokenRepository
{
    Task DeleteAsync(long id);
    Task DeleteAsync(PasswordToken Entity);
    Task<PasswordToken> GetByIdAsync(long id);
    Task<PasswordToken> AddAsync(PasswordToken Entity);
}
