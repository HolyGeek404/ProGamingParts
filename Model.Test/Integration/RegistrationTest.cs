using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Contexts;
using Model.DataAccess;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Models.General;
using Model.Services.Interfaces;
using Model.Services.User;
using NUnit.Framework;

namespace Model.Test.Integration;

[TestFixture, Explicit("Real DB call")]
public class RegistrationTest
{
    private ConfigurationManager _configuration { get; set; }
    private DbContextOptions<PGPContext> _options { get; set; }
    private IValidationService _validationService { get; set; }
    private IUserDao _userDao { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _validationService = new ValidationService();
        _options = new DbContextOptions<PGPContext>();
        _configuration = new ConfigurationManager();
        _configuration.AddJsonFile("appsettings.json");
        _userDao = new UserDao(_options);
    }


    [Test]
    public void RegisterValidUser_ShouldInsertUser()
    {
        //Arr
        using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled);

        var registrationForm = new RegistrationFormModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            Password = "Password1!",
            Password2 = "Password1!"
        };
        var userDto = new UserDto
        {
            Email = "test@example.com",
            Name = "Test",
            Surname = "User",
            Password = "password"
        };

        //Act
        var validationResult = _validationService.ValidateRegistrationData(registrationForm);
        var isUserAlreadyExist = _userDao.IsThisUserAlreadyExist(registrationForm.Email);

        if (validationResult && !isUserAlreadyExist)
        {
            _userDao.RegisterUser(userDto);

        }

        var result = _userDao.GetUserByEmail(userDto.Email);

        //Ass
        Assert.That(result, Is.Not.Null);
        Assert.That(validationResult, Is.True);
        Assert.That(isUserAlreadyExist, Is.False);
    }

    [Test]
    public void RegisterInvalidUser_ShouldNotInsertInvalidUser()
    {
        //Arr
        using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled);

        var registrationForm = new RegistrationFormModel
        {
            Email = "invalid-email",
            FirstName = "John",
            LastName = "",
            Password = "Password1!",
            Password2 = "Password1!"
        };
        var userDto = new UserDto
        {
            Email = "invalid-email",
            Name = "John",
            Surname = "",
            Password = "password"
        };

        //Act
        var validationResult = _validationService.ValidateRegistrationData(registrationForm);
        var isUserAlreadyExist = _userDao.IsThisUserAlreadyExist(registrationForm.Email);

        if (validationResult && !isUserAlreadyExist)
        {
            _userDao.RegisterUser(userDto);
        }

        var result = _userDao.GetUserByEmail(userDto.Email);

        //Ass
        Assert.That(validationResult, Is.False);
        Assert.That(isUserAlreadyExist, Is.False);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void RegisterInvalidUser_ShouldNotInsertAlreadyExistedUser()
    {
        //Arr
        using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled);

        var registrationForm = new RegistrationFormModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            Password = "Password1!",
            Password2 = "Password1!"
        };
        var userDto = new UserDto
        {
            Email = "test@example.com",
            Name = "Test",
            Surname = "User",
            Password = "password"
        };
        var userDto2 = new UserDto
        {
            Email = "test@example.com",
            Name = "Bob",
            Surname = "One",
            Password = "password"
        };

        //Act
        _userDao.RegisterUser(userDto2);

        var validationResult = _validationService.ValidateRegistrationData(registrationForm);
        var isUserAlreadyExist = _userDao.IsThisUserAlreadyExist(registrationForm.Email);

        if (validationResult && !isUserAlreadyExist)
        {
            _userDao.RegisterUser(userDto);
        }

        var result = _userDao.GetUserByEmail(userDto.Email);

        //Ass
        Assert.That(validationResult, Is.True);
        Assert.That(isUserAlreadyExist, Is.True);
        Assert.That(result, Is.Not.Null);
    }
}