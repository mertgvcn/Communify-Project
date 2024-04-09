using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository.Interfaces
{
    internal interface IBaseRepository<T> where T : BaseEntity
    {
        Task DeleteAsync(long id);
        Task DeleteAsync(T Entity);
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(long id);
        Task UpdateAsync(T Entity);
        Task<T> AddAsync(T Entity);
    }
}