using System.Reflection;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Model.Services.Interfaces;

namespace Model.Services;

public class UserService(IUserDao userDao, IHashService hashService, IHttpContextAccessor context) : IUserService
{
    public bool LogIn(string email, string password)
    {
        var userDto = userDao.GetUser(email);

        if (userDto.Email.IsNullOrEmpty() || !hashService.VerifyPassword(password, userDto.Password))
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
        var userData = userDao.RegisterUser(userDto);
        
        var productProperties = userData.GetType().GetProperties();
        CreateUserSession(productProperties, userData);
        
        return true;
    }

    public string HashPassword(string password)
    {
        return hashService.Hash(password);
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
}