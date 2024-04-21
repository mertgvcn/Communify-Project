using CommunifyLibrary.Models;
using CommunifyLibrary.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary.Repository;
public class PasswordTokenRepository(CommunifyContext context) : BaseRepository<PasswordToken>(context), IPasswordTokenRepository
{
    public async Task<PasswordToken?> GetByTokenAsync(string token)
        => await GetAll().Where(x => x.Token == token).FirstOrDefaultAsync();

    public async Task<bool> PasswordTokenExistsAsync(string token)
        => await GetAll().Where(x => x.Token == token).AnyAsync();
}
