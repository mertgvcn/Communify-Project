namespace LethalCompany_Backend.Services.Interfaces;

public interface ICryptionService
{
    Task<string> Decrypt(string key);
}