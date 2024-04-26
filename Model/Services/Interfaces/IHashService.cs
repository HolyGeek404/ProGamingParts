namespace Model.Services.Interfaces;

public interface IHashService
{
    string Hash(string input);
    bool VerifyPassword(string passwordForm, string passwordHash);
}