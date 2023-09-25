using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SEP_Web.Models;
public class Login
{
    [Required(ErrorMessage = "Informe seu MASP.")]
    public int? Masp { get; set; }

    [Required(ErrorMessage = "Informe seu nome de login.")]
    public string LoginName { get; set; }

    [Required(ErrorMessage = "Informe sua senha.")]
    public string Password { get; set; }

    public static void FieldsValidation(string fields, string errorMessage, Controller controller)
    {
        controller.TempData[fields] = errorMessage;
    }
}
