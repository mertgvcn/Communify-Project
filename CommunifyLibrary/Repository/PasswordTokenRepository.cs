using CommunifyLibrary.Models;
using CommunifyLibrary.Repository.Interfaces;
using LethalCompany_Backend.NonPersistentModels.TokenModels;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary.Repository;
public class PasswordTokenRepository(CommunifyContext context) : BaseRepository<PasswordToken>(context), IPasswordTokenRepository
{
    public async Task<PasswordToken?> GetByTokenAsync(string token)
        => await GetAll().Where(x => x.Token == token).FirstOrDefaultAsync();

    public async Task<bool> PasswordTokenExistsAsync(PasswordTokenExists request)
    {
        return await GetAll().Where(x => x.Token == request.Token).AnyAsync();
    }
}
