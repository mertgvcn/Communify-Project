using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        Task<User> GetByIdAsync(long id);
        Task UpdateAsync(User user);
        Task<User> AddAsync(User Entity);
        IQueryable<User> GetByEmail(string email);
        Task AddInterest(long userId, Interest interest);
    }
}