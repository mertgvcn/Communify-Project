using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunifyLibrary.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.DateCreated)
                .IsRequired(true);

            builder.Property(x => x.DateModified)
                .IsRequired(false);

            builder.Property(x => x.IsDeleted)
                .IsRequired(true)
                .HasDefaultValue(false);
        }
    }
}
