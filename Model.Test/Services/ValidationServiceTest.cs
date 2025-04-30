using Model.Models.General;
using Model.Services.User;
using NUnit.Framework;

namespace Model.Test.Services;

public class ValidationServiceTests
{
    private ValidationService _validationService;

    [SetUp]
    public void Setup()
    {
        _validationService = new ValidationService();
    }

    [Test]
    [TestCase("test@example.com", true)]
    [TestCase("invalid-email", false)]
    public void ValidateEmail(string email, bool expected)
    {
        var result = _validationService.ValidateEmail(email);
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    [TestCase("John", "Doe", true)]
    [TestCase("john", "doe", false)]
    public void ValidateNameAndSurname(string firstName, string lastName, bool expected)
    {
        var result = _validationService.ValidateNameAndSurname(firstName, lastName);
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    [TestCase("Password1!", "Password1!", true)]
    [TestCase("password", "password", false)]
    [TestCase("Password1!", "Password2!", false)]
    public void ValidatePassword(string password1, string password2, bool expected)
    {
        var result = _validationService.ValidatePassword(password1, password2);
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void ValidateRegistrationData_ShouldReturnFalse_WhenFieldsAreEmpty()
    {
        var registrationForm = new RegistrationFormModel
        {
            Email = "test@example.com",
            LastName = "Doe"
        };
        var result = _validationService.ValidateRegistrationData(registrationForm);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateRegistrationData_ShouldReturnTrue_WhenAllFieldsAreValid()
    {
        var registrationForm = new RegistrationFormModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            Password = "Password1!",
            Password2 = "Password1!"
        };
        var result = _validationService.ValidateRegistrationData(registrationForm);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateRegistrationData_ShouldReturnFalse_WhenAnyFieldIsInvalid()
    {
        var registrationForm = new RegistrationFormModel
        {
            Email = "invalid-email",
            FirstName = "John",
            LastName = "Doe",
            Password = "Password1!",
            Password2 = "Password1!"
        };
        var result = _validationService.ValidateRegistrationData(registrationForm);
        Assert.That(result, Is.False);
    }
}