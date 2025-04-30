using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Entities;

namespace Model.DataAccess;

public class UserDao(DbContextOptions<PGPContext> options) : BaseDao(options), IUserDao
{
    public UserDto? GetUserByEmail(string email)
    {
        var result = PgpContext.Users.Where(x => x.Email == email)
            .Select(x => new UserDto()
            {
                UserId = x.UserId,
                UserName = x.Name,
                Email = x.Email,
                Password = x.Password,
                Name = x.Name,
                Surname = x.Surname,
                Type = x.Type,
                IsActive = x.IsActive,
            }).SingleOrDefault();

        if (result == null) return null;
        {
            result.DeliveryAddress = PgpContext.Addresses.SingleOrDefault(x => x.UserId == result.UserId);

            return result;
        }
    }
    public UserDto? GetUserById(string userId)
    {

        var result = PgpContext.Users.Where(x => x.UserId == int.Parse(userId))
            .Select(x => new UserDto()
            {
                UserId = x.UserId,
                UserName = x.Name,
                Email = x.Email,
                Password = x.Password,
                Name = x.Name,
                Surname = x.Surname,
                Type = x.Type,
                IsActive = x.IsActive,
            }).FirstOrDefault();

        return result;

    }
    public UserDto RegisterUser(UserDto userDto, string userType = "User")
    {
        PgpContext.Users.Add(new User
        {
            UserId = userDto.UserId,
            Email = userDto.Email,
            Name = userDto.Name,
            Surname = userDto.Surname,
            Password = userDto.Password,
            Type = userType,
            IsActive = false,
            Key = userDto.Key
        });
        PgpContext.SaveChanges();

        return userDto;

    }
    public bool IsThisUserAlreadyExist(string email)
    {
        return PgpContext.Users.Count(x => x.Email == email) > 0;
    }
    public void UpdateSettings(string UserId, string? Email = null, string? Name = null, string? Surname = null, string? Password = null)
    {
        var userId = int.Parse(UserId);

        var user = PgpContext.Users.SingleOrDefault(u => u.UserId == userId);
        if (user == null) return;

        if (!string.IsNullOrEmpty(Email))
            user.Email = Email;

        if (!string.IsNullOrEmpty(Name))
            user.Name = Name;

        if (!string.IsNullOrEmpty(Surname))
            user.Surname = Surname;

        if (!string.IsNullOrEmpty(Password))
            user.Password = Password;

        PgpContext.SaveChanges();
    }

    public void DeleteUser(string userId)
    {
        PgpContext.Users.Where(x => x.UserId == int.Parse(userId)).ExecuteDelete();
    }
    public bool VerifyAccount(string userEmail, Guid key)
    {
        var user = PgpContext.Users.SingleOrDefault(x => x.Email == userEmail && x.Key == key);

        if (user == null) return false;

        user.IsActive = true;
        user.Key = null;

        PgpContext.SaveChanges();

        return true;
    }

    public void SaveDeliveryAddress(DeliveryAddress address)
    {
        var hasAddressAlready = PgpContext.Addresses.SingleOrDefault(x => x.UserId == address.UserId);

        if (hasAddressAlready == null)
        {
            PgpContext.Addresses.Add(address);
        }
        else
        {
            hasAddressAlready.ApartmentNumber = address.ApartmentNumber;
            hasAddressAlready.Email = address.Email;
            hasAddressAlready.Name = address.Name;
            hasAddressAlready.BlockNumber = address.BlockNumber;
            hasAddressAlready.City = address.City;
            hasAddressAlready.PostalCode = address.PostalCode;
            hasAddressAlready.Surname = address.Surname;
            hasAddressAlready.Street = address.Street;
        }

        PgpContext.SaveChanges();
    }

    public DeliveryAddress? GetDefaultDeliveryAddress(string userId)
    {
        return PgpContext.Addresses.SingleOrDefault(x => x.UserId == int.Parse(userId));
    }

    public List<UserDto> LoadUsers()
    {
        var userDtoList = PgpContext.Users.Select(x => new UserDto
        {
            UserId = x.UserId,
            Email = x.Email,
            Name = x.Name,
            Surname = x.Surname
        }).ToList();
        return userDtoList;
    }
}