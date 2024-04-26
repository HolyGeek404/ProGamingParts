using DataAccess.DTO;

namespace DataAccess.DAO.Interfaces;

public interface IUserDao
{
    UserDto GetUser(string email);
    UserDto RegisterUser(UserDto userDto);
    bool IsThisUserAlreadyExist(string email);
}