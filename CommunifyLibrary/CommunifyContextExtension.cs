using CommunifyLibrary.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CommunifyLibrary
{
    public static class CommunifyContextExtension
    {
        public static void BuildConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new InterestConfiguration());
        }
    }
}
