using CommunifyLibrary.Models;
using CommunifyLibrary.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary;

public class CommunifyContext(DbContextOptions<CommunifyContext> options) : DbContext(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<PasswordToken> PasswordTokens { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries().Where(a => a.State == EntityState.Modified))
        {
            if (item is not IEditableEntity)
            {
                throw new InvalidOperationException("Cannot modify entity because its not editable");
            }

        }
        foreach (var item in ChangeTracker.Entries().Where(a => a.State == EntityState.Deleted))
        {
            if (item is ISoftDeletableEntity)
            {
                item.State = EntityState.Modified;
                var obj = (ISoftDeletableEntity)item.Entity;
                Attach(obj);
                obj.IsDeleted = true;
            }
            else if (item is not IDeletableEntity)
            {
                throw new InvalidOperationException("Cannot delete entity because its not deletable");
            }
        }
        await SaveChangesAsync();


        return 1;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildConfigurations();
        modelBuilder.Entity<User>().HasMany(a => a.Followers).WithMany(a => a.Followings).UsingEntity(a => a.ToTable("Followings"));


        base.OnModelCreating(modelBuilder);
    }

}
