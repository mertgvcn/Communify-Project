using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunifyLibrary.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.LastName)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.Username)
                .IsRequired(true)
                .HasMaxLength(32);

            builder.Property(x => x.BirthDate)
                .IsRequired(true);

            builder.Property(x => x.BirthCountry)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.BirthCity)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.CurrentCountry)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.CurrentCity)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.Gender)
                .IsRequired(true);

            builder.Property(x => x.Address)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(x => x.PhoneNumber)
                .IsRequired(true)
                .HasMaxLength(16);

            builder.Property(x => x.Email)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(x => x.Password)
                .IsRequired(false)
                .HasMaxLength(256);
        }
    }
}
