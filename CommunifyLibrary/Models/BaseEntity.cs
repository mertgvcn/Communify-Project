using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunifyLibrary.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime DateCreated { get; } = DateTime.Now;

        public DateTime? DateModified { get; set; }

        public bool IsDeleted { get; set; }
    }
}
