using DataAccess.DTO;

namespace Model.Services.Interfaces;

public interface IUserService
{
    bool LogIn(string email, string password);
    void LogOut();
    bool Register(UserDto userDto);
    string HashPassword(string password);
} 