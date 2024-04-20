﻿using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        IQueryable<User> GetByEmail(string email);
        Task<User> GetByIdAsync(long id);
        Task<long> GetIdByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task<User> AddAsync(User Entity);
        Task AddInterest(long userId, Interest interest);
    }
}