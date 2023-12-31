using Microsoft.AspNetCore.Mvc;
using SEP_Web.Models;

namespace SEP_Web.Helper.Validations;

public interface IValidationUsers
{
    Task<bool> VerifyIfFieldExistsInBothUsersTable(string fieldName, object value);
    void LoginFieldsValidation(string fields, string errorMessage, Controller controller);
    bool ValidatePassword(string pass, string confirmPass, Controller controller);
    bool IsFieldChanged(Users existingUser, string fieldName, object newValue);
}
