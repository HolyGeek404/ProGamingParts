using Dapper;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class UserDao : BaseDao, IUserDao
{
    public UserDao(IConfiguration configuration) : base(configuration)
    { }

    public UserDto GetUser(string email)
    {
        return DbConnection.QuerySingle<UserDto>("dbo.User_GetUser @Email", new {Email = email});
    }

    public UserDto RegisterUser(UserDto userDto)
    {
        return DbConnection.QuerySingle<UserDto>("dbo.User_RegisterUser @Email, @Name, @Surname, @Password", new
        {
            Email = userDto.Email,
            Name = userDto.Name,
            Surname = userDto.Surname,
            Password = userDto.Password
        });
    }

    public bool IsThisUserAlreadyExist(string email)
    {
        var x = DbConnection.QuerySingle<bool>("dbo.User_IsThisUserAlreadyExist @email", new { email });
        return x;
    }
}