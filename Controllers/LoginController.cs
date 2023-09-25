using Microsoft.AspNetCore.Mvc;
using SEP_Web.Auth;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserAdministratorServices _usersServices;
    private readonly IUserSession _session;

    public LoginController(ILogger<LoginController> logger, IUserAdministratorServices usersServices, IUserSession session)
    {
        _logger = logger;
        _usersServices = usersServices;
        _session = session;
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
                UserAdministrator users = await _session.UserSignIn(login.Masp, login.LoginName); // UserSignIn é chamado para armazenar o usuário correspondente em um objeto de Usuários

                if (users != null) // valida se o objeto é nullo
                {
                    if (login.Masp != users.Masp)
                    {
                        Login.FieldsValidation("InvalidMASP", "O MASP informado é inválido", this);
                        return View("Index");
                    }

                    if (login.LoginName != users.Login)
                    {
                        Login.FieldsValidation("InvalidLogin", "O login informado é inválido", this);
                        return View("Index");
                    }

                    if (!users.ValidPassword(login.Password)) //valida o retorno da chamada do método ValidPassword que verifica a autenticidade do salt da senha do usuário para validar a senha e permitir o acesso.
                    {
                        Login.FieldsValidation("InvalidPass", "A senha informada é inválida", this);
                        return View("Index");
                    }
                    else
                    {
                        _session.UserCheckIn(users); // Estabelece a conexão do usuário através do método UserCheckIn que Serializa o objeto passado e redireciona o usuário para a devida página com sua sessão estabelecida.
                        return RedirectToAction("Index", "Home");
                    }

                }

                TempData["ErrorMessage"] = $"Os dados informados são inválidos. Corrija-os e tente novamente.";
            }

            return View("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = $"Não foi possível realizar o login. Por favor tente novamente mais tarde !";
            _logger.LogError("Não foi possível realizar o login", e.Message);
            return RedirectToAction("Index");
        }
    }
    public IActionResult Logout()
    {
        _session.UserCheckOut(); // finaliza a sessão através da chamada de UserCheckOut que remove os dados da sessão ao desserializar o objeto
        return RedirectToAction("Index", "Login");
    }
}
