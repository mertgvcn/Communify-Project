namespace CommunifyLibrary.NonPersistentModels.ConfigurationModels;

public class MailConfig
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string SenderName { get; set; }

    public string SenderMail { get; set; }

    public string SenderPassword { get; set; }
}
