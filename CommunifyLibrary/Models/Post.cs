using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunifyLibrary.Models
{
    public class Post: BaseEntity
    {
        [MaxLength(300)]
        public string postName { get; set; }
    }
}
