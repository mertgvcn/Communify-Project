using CommunifyLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunifyLibrary.Configurations
{
    public class PostConfiguration: BaseEntityConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);
        }
    }
}
