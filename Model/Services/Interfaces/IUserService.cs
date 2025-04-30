using Model.DataTransfer;
using Model.Entities;

namespace Model.Services.Interfaces;

public interface IUserService
{
    bool LogIn(string email, string password);
    void LogOut();
    bool Register(UserDto userDto);
    string HashPassword(string password);
    UserDto GetUserByEmail(string email);
    UserDto GetUserById(string userId);
    bool UpdateNameAndSurnameSetting(string userId, string name, string surname);
    bool UpdatePasswordSetting(string password, string currentEmail);
    bool UpdateEmailSetting(string userId, string email);
    List<UserDto> LoadUsers();
    void DeleteUser(string userId);
    void SendVerficationEmail(string userDtoEmail, Guid newGuid);
    void SaveDeliveryAddress(DeliveryAddress address);
} 