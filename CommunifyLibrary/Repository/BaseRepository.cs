using CommunifyLibrary.Models;
using CommunifyLibrary.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary.Repository
{
    public abstract class BaseRepository<T>(CommunifyContext _context) : IBaseRepository<T> where T : BaseEntity
    {
        public IQueryable<T> GetAll() => _context.Set<T>().AsQueryable();

        public async Task<T> GetByIdAsync(long id) => await GetAll().Where(a => a.Id == id).SingleAsync();

        public async Task DeleteAsync(long id)
        {
            await DeleteAsync(await GetByIdAsync(id));
        }

        public async Task DeleteAsync(T Entity)
        {
            _context.Remove(Entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T Entity)
        {
            _context.Attach(Entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> AddAsync(T Entity)
        {
            var Entry = await _context.AddAsync(Entity); //return the object that has been added
            await _context.SaveChangesAsync();

            return Entry.Entity;
        }
    }
}
