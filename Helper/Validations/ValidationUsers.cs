using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Helper.Validations;
public class ValidationUsers : IValidationUsers
{
    private readonly SEP_WebContext _database;

    public ValidationUsers(SEP_WebContext database)
    {
        _database = database;
    }

    public async Task<bool> VerifyIfFieldExistsInBothUsersTable(string fieldName, object value)
    {
        bool usersAdministrators = await _database.Administrator.AnyAsync(u => EF.Property<object>(u, fieldName) == value);
        bool usersEvaluators = await _database.Evaluator.AnyAsync(u => EF.Property<object>(u, fieldName) == value);

        return usersAdministrators || usersEvaluators;
    }

    public bool ValidatePassword(string pass, string confirmPass, Controller controller)
    {
        if (confirmPass != pass)
        {
            controller.TempData["ErrorPass"] = "As senhas são diferentes.";
            return false;
        }

        return true;
    }

    public void LoginFieldsValidation(string fields, string errorMessage, Controller controller)
    {
        controller.TempData[fields] = errorMessage;
    }

    public bool IsFieldChanged(Users existingUser, string fieldName, object newValue)
    {
        // Verifique se o valor dos campos em um usuário já existente é diferente do novo valor (considerando nulos);
        // Método de auxílio da edição de usuários administradores;
        var existingValue = existingUser.GetType().GetProperty(fieldName).GetValue(existingUser);
        return !Equals(existingValue, newValue);
    }
}