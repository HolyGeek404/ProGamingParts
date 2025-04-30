using System.Reflection;
using Microsoft.AspNetCore.Http;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Entities;
using Model.Services.Interfaces;

namespace Model.Services.User;

public class UserService(IUserDao userDao, IHashService hashService, IHttpContextAccessor context, IEmailService emailService) : IUserService
{
    public bool LogIn(string email, string password)
    {
        var userDto = userDao.GetUserByEmail(email);

        if (userDto == null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(userDto.Email))
        {
            return false;
        }

        if (!hashService.VerifyPassword(password, userDto.Password))
        {
            return false;
        }

        if (!userDto.IsActive)
        {
            return false;
        }

        var productProperties = userDto.GetType().GetProperties();
        CreateUserSession(productProperties, userDto);

        return true;
    }
    public void LogOut()
    {
        context.HttpContext?.Session.Clear();
    }
    public bool Register(UserDto userDto)
    {
        if (userDao.IsThisUserAlreadyExist(userDto.Email)) return false;
        userDao.RegisterUser(userDto);

        return true;
    }
    public string HashPassword(string password)
    {
        return hashService.Hash(password);
    }
    public UserDto GetUserByEmail(string email)
    {
        return userDao.GetUserByEmail(email);
    }
    public UserDto GetUserById(string userId)
    {
        return userDao.GetUserById(userId);
    }
    private void CreateUserSession(IEnumerable<PropertyInfo> productProperties, UserDto userDto)
    {
        foreach (var property in productProperties)
        {
            var parameterValue = property.GetValue(userDto)?.ToString();
            if (string.IsNullOrEmpty(parameterValue)) continue;
            var parameterName = property.Name;

            context.HttpContext?.Session.SetString(parameterName, parameterValue);
        }
    }
    public bool UpdateNameAndSurnameSetting(string userId,string name, string surname)
    {
        userDao.UpdateSettings(UserId: userId, Name: name, Surname: surname);
        return true;
    }

    public bool UpdatePasswordSetting(string password, string currentEmail)
    {
        var hashedPassword = hashService.Hash(password);
        userDao.UpdateSettings(currentEmail, Password: hashedPassword);
        return true;
    }

    public bool UpdateEmailSetting(string userId, string email)
    {
        userDao.UpdateSettings(UserId: userId, Email: email);
        return true;
    }

    public List<UserDto> LoadUsers()
    {
        return userDao.LoadUsers();
    }

    public void DeleteUser(string userId)
    {
        userDao.DeleteUser(userId);
    }

    public void SendVerficationEmail(string userDtoEmail, Guid key)
    {
        emailService.SendVerficationEmail(userDtoEmail, key);
    }

    public void SaveDeliveryAddress(DeliveryAddress address)
    {
        userDao.SaveDeliveryAddress(address);
    }
}