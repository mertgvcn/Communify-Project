using CommunifyLibrary.Models;
using LethalCompany_Backend.NonPersistentModels.TokenModels;

namespace CommunifyLibrary.Repository.Interfaces;
public interface IPasswordTokenRepository
{
    Task DeleteAsync(long id);
    Task DeleteAsync(PasswordToken Entity);
    Task<PasswordToken> GetByIdAsync(long id);
    Task<PasswordToken> AddAsync(PasswordToken Entity);
    Task<PasswordToken?> GetByTokenAsync(string token);
    Task<bool> PasswordTokenExistsAsync(PasswordTokenExists token);
}
