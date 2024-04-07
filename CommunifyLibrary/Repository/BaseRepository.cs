using CommunifyLibrary.Models;
using CommunifyLibrary.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly CommunifyContext _context;

        public BaseRepository(CommunifyContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll() => _context.Set<T>().AsQueryable(); //shorter code for single line return function

        public async Task<T> GetByIdAsync(long id) => await GetAll().Where(a => a.Id == id).SingleAsync(); //if there is no object with given id, it will return an exception

        public async Task DeleteAsync(long id)
        {
            _context.Remove(await GetByIdAsync(id)); //if error occurs on delete, change to _context.Set<T>().Remove...
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
