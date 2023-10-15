using Microsoft.AspNetCore.Mvc;
using SEP_Web.Helper.Authentication;
using SEP_Web.Helper.Exceptions.Messages.FeedbackMessages;
using SEP_Web.Helper.Validations;
using SEP_Web.Models;

namespace SEP_Web.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserSession _session;
    private readonly IValidationUsers _validation;

    public LoginController(ILogger<LoginController> logger, IUserSession session, IValidationUsers validation)
    {
        _logger = logger;
        _session = session;
        _validation = validation;
    }

    public async Task<IActionResult> Index()
    {
        // A action result de index Login chama o método "SearchUserSession" para verificar se a sessão é nula e redireciona o usuário de acordo com o retorno da condição.
        if ( await _session.SearchUserSession() != null) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(Login login)
    {
        // Estabelece uma conexão para o usuário e inicia-se uma sessão para o mesmo;
        try
        {
            if (ModelState.IsValid) // valida o modelo de dados
            {
                Users users = await _session.UserSignIn(login.Masp, login.LoginName); // UserSignIn busca por um usuário que corresponda aos dados e o armazena em um objeto do tipo "Users", que é uma classe base para as demais entidades de usuários;

                if (!(users == null)) // verifica se o objeto não é nullo;
                {
                    // faz a validação entre os dados informados pelo o usuário e os dados correspondente ao usuário que foi encontrado para que seja verificado a autenticidade do mesmo;
                    if (login.Masp != users.Masp)
                    {
                        _validation.LoginFieldsValidation("InvalidMASP", "O MASP informado é inválido", this);
                        return View("Index");
                    }

                    if (login.LoginName != users.Login)
                    {
                        _validation.LoginFieldsValidation("InvalidLogin", "O login informado é inválido", this);
                        return View("Index");
                    }

                    if (!Cryptography.VerifyPasswordEncrypted(login.Password, users.Password)) // Valida a senha do usuário com base na verificação do salto da hash única que é gerada no momento da criação da senha, comparando-o a senha informada;
                    {
                        _validation.LoginFieldsValidation("InvalidPass", "A senha informada é inválida", this);
                        return View("Index");
                    }
                    else
                    {
                        _session.UserCheckIn(users); // Utiliza o método UserCheckIn que serializa um objeto de usuário com base no objeto que foi recebido e validado. Também seta uma váriavel de sessão para capturar o  tipo do usuário;
                        return RedirectToAction("Index", "Home");
                    }

                }

                TempData["ErrorMessage"] = ExceptionsFeedbackMessage.InvalidData; 

            }
            return View("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = $"{ExceptionsFeedbackMessage.ErrorDatabaseConnection} {ExceptionsFeedbackMessage.ErrorLogin}";
            _logger.LogError(ExceptionsFeedbackMessage.ErrorLogin, e.Message);
            return RedirectToAction("Index");
        }
    }
    public IActionResult Logout()
    {
        _session.UserCheckOut(); // UserCheckOut finaliza e remove os dados da sessão;
        return RedirectToAction("Index", "Login");
    }
}
