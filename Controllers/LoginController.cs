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

    public IActionResult Index()
    {
        // A action result de index Login valida se a sessão é nula e redireciona o usuário de acordo com o retorno da condição.
        if (_session.SearchUserSession() != null) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(Login login) // Loga o usuário e estabelece para o mesmo uma sessão
    {
        try
        {
            if (ModelState.IsValid) // valida o modelo de dados
            {
                Users users = await _session.UserSignIn(login.Masp, login.LoginName); // UserSignIn é chamado para armazenar o usuário correspondente em um objeto de Usuários

                if (!(users == null)) // valida se o objeto é nullo
                {
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

                    if (!Cryptography.VerifyPasswordEncrypted(login.Password, users.Password)) //valida o retorno da chamada do método VerifyPasswordEncrypted que verifica a autenticidade do salt da senha do usuário para validar a senha e permitir o acesso.
                    {
                        _validation.LoginFieldsValidation("InvalidPass", "A senha informada é inválida", this);
                        return View("Index");
                    }
                    else
                    {
                        _session.UserCheckIn(users); // Estabelece a conexão do usuário através do método UserCheckIn que Serializa o objeto passado e redireciona o usuário para a devida página com sua sessão estabelecida.
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
        _session.UserCheckOut(); // finaliza a sessão através da chamada de UserCheckOut que remove os dados da sessão ao desserializar o objeto
        return RedirectToAction("Index", "Login");
    }
}
