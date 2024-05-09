using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary.Repository;

public class UserRepository(CommunifyContext context, IInterestRepository interestRepository) : BaseRepository<User>(context), IUserRepository
{
    public IQueryable<User> GetByEmail(string email) => context.Users.Where(a => a.Email == email);
    public IQueryable<User> GetByUsername(string username) => context.Users.Where(a => a.Username == username);
    public IQueryable<User> SearchUsers(string input)
    {
        input = input.ToLower();

        return context.Users
            .Where(a => a.isActive == true && (String.Concat(a.FirstName.ToLower(), " ", a.LastName.ToLower()).Contains(input) || a.Username.Contains(input)))
            .OrderBy(a => a.FirstName);
    }

    public async Task<long> GetIdByEmailAsync(string email) => (await GetByEmail(email).SingleAsync()).Id;

    public async Task AddInterest(long userId, long interestId)
    {
        var user = await GetAll().Where(a => a.Id == userId)
            .Include(a => a.Interests)
            .Include(a => a.Followers)
            .Include(a => a.Followings)
            .SingleAsync();
        Interest interest = await interestRepository.GetByIdAsync(interestId);
        var a = await GetByIdAsync(54);
        var b = await GetByIdAsync(54);

        user.Followers.Add(a);
        user.Followings.Add(b);
        user.Interests.Add(interest);
        await context.SaveChangesAsync();
    }

}

