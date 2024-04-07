using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public interface IInterestRepository
    {
        IQueryable<Interest> GetAll();
        Task<Interest> GetByIdAsync(long id);
        Task<Interest> AddAsync(Interest Entity);
    }
}