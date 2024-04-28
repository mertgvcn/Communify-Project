using CommunifyLibrary.Models;

namespace CommunifyLibrary.NonPersistentModels.ParameterModels;

public class GenerateTokenRequest
{
    public string UserID { get; set; }

    public Role Role { get; set; }

    public DateTime ExpireDate { get; set; }
}
