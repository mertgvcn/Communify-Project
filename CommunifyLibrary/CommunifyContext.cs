using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CommunifyLibrary
{
    public class CommunifyContext : DbContext
    {
        public CommunifyContext(DbContextOptions<CommunifyContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } 

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildConfigurations();

            base.OnModelCreating(modelBuilder);
        }

    }
}
