namespace Model.Services.Interfaces;

public interface IEmailService
{
    void SendVerficationEmail(string userEmail, Guid key);
}