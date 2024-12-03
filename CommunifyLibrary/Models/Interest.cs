using CommunifyLibrary.Models.Interfaces;

namespace CommunifyLibrary.Models
{
    public class Interest : BaseEntity, IEditableEntity, ISoftDeletableEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        public string EditedBy { get; set; }
        public DateTime LastChanges { get; set; }
    }
}
