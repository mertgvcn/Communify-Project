using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public class UserRepository(CommunifyContext context) : BaseRepository<User>(context), IUserRepository
    {
        public IQueryable<User> GetByEmail(string email) => context.Users.Where(a => a.Email == email).AsQueryable();

        public async Task AddInterest(long userId, Interest interest)
        {
            var user = await GetByIdAsync(userId);

            if (user.Interests == null)
                user.Interests = new List<Interest>();

            user.Interests.Add(interest);
            await context.SaveChangesAsync();
        }
    }
}
