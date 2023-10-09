using Microsoft.AspNetCore.Mvc;

namespace SEP_Web.Helper.Validations;

public interface IValidationUsers
{
    Task<bool> VerifyIfFieldExistsInBothUsersTable(string fieldName, object value);
    bool ValidatePassword(string pass, string confirmPass, Controller controller);
}
