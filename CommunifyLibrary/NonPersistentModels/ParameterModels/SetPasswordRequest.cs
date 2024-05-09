namespace CommunifyLibrary.NonPersistentModels.ParameterModels;

public class SetPasswordRequest
{
    public string Token { get; set; }

    public string Password { get; set; }
}
