namespace CommunifyLibrary.Models;
public class Notification : BaseEntity
{
    public string Message { get; set; } = default!;
    public bool Seen { get; set; }
}
