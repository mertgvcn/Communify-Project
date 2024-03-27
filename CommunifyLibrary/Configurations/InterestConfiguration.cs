using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunifyLibrary.Configurations
{
    public class InterestConfiguration : BaseEntityConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(128);
        }
    }
}
