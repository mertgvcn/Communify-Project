using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunifyLibrary.Configurations
{
    public class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(128);
        }
    }
}
