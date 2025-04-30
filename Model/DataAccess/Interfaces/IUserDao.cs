using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess.Interfaces;

public interface IUserDao
{
    UserDto? GetUserByEmail(string email);
    UserDto? GetUserById(string userId);
    List<UserDto> LoadUsers();
    public UserDto RegisterUser(UserDto userDto, string userType = "User");
    bool IsThisUserAlreadyExist(string email);

    void UpdateSettings(string UserId, string? Email = null, string? Name = null, string? Surname = null,
        string? Password = null);

    void DeleteUser(string userId);
    bool VerifyAccount(string userEmail, Guid key);
    void SaveDeliveryAddress(DeliveryAddress address);
    DeliveryAddress? GetDefaultDeliveryAddress(string userId);
}