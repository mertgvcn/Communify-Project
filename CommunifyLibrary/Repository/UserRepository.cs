using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public class UserRepository(CommunifyContext context) : BaseRepository<User>(context), IUserRepository
    {
        public IQueryable<User> GetByEmail(string email) => context.Users.Where(a => a.Email == email).AsQueryable();

        public async Task AddInterest(long userId, Interest interest)
        {
            (await GetByIdAsync(userId)).Interests.Add(interest);
            await context.SaveChangesAsync();
        }
    }
}
