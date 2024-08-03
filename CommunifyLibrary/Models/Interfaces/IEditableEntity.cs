namespace CommunifyLibrary.Models.Interfaces;
public interface IEditableEntity
{
    public string EditedBy { get; set; }
    public DateTime LastChanges { get; set; }
}
