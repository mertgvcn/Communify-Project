using CommunifyLibrary.Models.Interfaces;

namespace CommunifyLibrary.Models;
public class Notification : BaseEntity, IDeletableEntity
{
    public string Message { get; set; } = default!;
    public bool Seen { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
