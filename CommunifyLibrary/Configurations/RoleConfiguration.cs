using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunifyLibrary.Configurations
{
    public class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(32);
        }
    }
}
