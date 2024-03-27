namespace CommunifyLibrary.Models
{
    public class Interest : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
