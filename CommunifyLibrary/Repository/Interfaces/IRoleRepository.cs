using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public interface IRoleRepository
    {
        IQueryable<Role> GetAll();
        Task<Role> GetByIdAsync(long id);
    }
}