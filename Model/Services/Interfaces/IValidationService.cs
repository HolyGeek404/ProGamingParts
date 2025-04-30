using Model.Models.General;

namespace Model.Services.Interfaces;

public interface IValidationService
{
    bool ValidateRegistrationData(RegistrationFormModel registrationFormModel);
    bool ValidatePassword(string password1, string password2);
    bool ValidateNameAndSurname(string name, string surname);
    bool ValidateEmail(string email);
}