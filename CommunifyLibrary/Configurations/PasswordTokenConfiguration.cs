using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunifyLibrary.Configurations;
public class PasswordTokenConfiguration : BaseEntityConfiguration<PasswordToken>
{
    public void Configure(EntityTypeBuilder<PasswordToken> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Token)
            .IsRequired(true)
            .HasMaxLength(65);

        builder.Property(x => x.UserId)
            .IsRequired(false);
    }
}
