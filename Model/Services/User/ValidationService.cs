using System.Text.RegularExpressions;
using Model.Models.General;
using Model.Services.Interfaces;

namespace Model.Services.User;

public class ValidationService : IValidationService
{
    private readonly Regex emailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"); 
    private readonly Regex nameAndSurnameRegex = new(@"^[A-Z][a-zA-Z\s'-]*$");
    private readonly Regex passUpperCaseRegex = new(@".*[A-Z].*");
    private readonly Regex digitRegex = new(@".*\d.*");
    private readonly Regex specialCharsRegex = new(@".*\W.*");

    public bool ValidateRegistrationData(RegistrationFormModel registrationFormModel)
    {
        if (string.IsNullOrWhiteSpace(registrationFormModel.Email)
                               || string.IsNullOrWhiteSpace(registrationFormModel.FirstName)
                               || string.IsNullOrWhiteSpace(registrationFormModel.Password)
                               || string.IsNullOrWhiteSpace(registrationFormModel.Password2)
                               || string.IsNullOrWhiteSpace(registrationFormModel.LastName))
        {
            return false;
        }

        var isEmailValid = ValidateEmail(registrationFormModel.Email);
        var isNameAndSurnameValid = ValidateNameAndSurname(registrationFormModel.FirstName, registrationFormModel.LastName);
        var isPasswordValid = ValidatePassword(registrationFormModel.Password, registrationFormModel.Password2);

        return isEmailValid && isNameAndSurnameValid && isPasswordValid;
    }
    public bool ValidateEmail(string email)
    {
        return emailRegex.IsMatch(email);
    }

    public bool ValidatePassword(string password1, string password2)
    {
        var isPasswordsValid = ValidatePassword(password1) && ValidatePassword(password2);
        return isPasswordsValid && string.Equals(password1, password2);
    }

    public bool ValidateNameAndSurname(string name, string surname)
    {
        return ValidateName(name) && ValidateSurname(surname);
    }

    private bool ValidateSurname(string surname)
    {
        return nameAndSurnameRegex.IsMatch(surname);
    }

    private bool ValidateName(string name)
    {
        return nameAndSurnameRegex.IsMatch(name);
    }

    private bool ValidatePassword(string password)
    {
        return passUpperCaseRegex.IsMatch(password) && digitRegex.IsMatch(password) && specialCharsRegex.IsMatch(password);
    }
}