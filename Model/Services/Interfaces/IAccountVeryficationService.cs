namespace Model.Services.Interfaces;

public interface IAccountVerifycationService
{
    bool VerifyAccount(string userEmail, Guid key);
}