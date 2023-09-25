using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class UserAdministratorController : Controller
{
    private readonly ILogger<UserAdministratorController> _logger;
    private readonly IUserAdministratorServices _usersServices;
    private readonly SEP_WebContext _database;

    public UserAdministratorController(ILogger<UserAdministratorController> logger, IUserAdministratorServices usersServices, SEP_WebContext databse)
    {
        _logger = logger;
        _usersServices = usersServices;
        _database = databse;
    }

    /* TODO: */
    // Resvar classe apenas para usuários que serão administradores para separar a lógica futura a ser implementada. 

    public async Task<IActionResult> Index()
    {
        /*Listagem geral de usuários cadastrados*/

        ICollection<UserAdministrator> users = await _usersServices.AdministratorsList();
        return View(users);
    }

    public IActionResult Register()
    {
        /*ActionResult que exibe a página de cadastro de usuários*/

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserAdministrator users, string confirmPass)
    {
        /* ActionResult responsável por realizar o post das informações que serão recebidas através do formulário de cadastro de usuários*/

        /* O método está envolvido em um bloco try catch para lidar com erros que possam ocorrer. Caso ocorra algum erro e uma excessão é lançada e devidamente tratada para retornar uma mensagem ao usuário informando sobre o erro, o bloco ainda armazena o erro gerado em um arquivo de log para que facilite a manutenção */
        try
        {
            if (ModelState.IsValid) // valida o modelo utilizado na requisição
            {
                var fieldsToValidate = new List<(string FieldName, object Value)>
                {
                    /* Com o modelo validado uma variável é criada recebendo uma lista como valor. a lista contêm parâmetros que serão usados na validação dos dados do usuário a fim de evitar a duplicação de dados obrigatótios no registro de usuários. A validação utilizará o nome do atributo a ser validado e terá também o valor do mesmo para que a validação seja efetiva.*/

                    ("Masp", users.Masp),
                    ("Name", users.Name),
                    ("Login", users.Login),
                    ("Email", users.Email),
                    ("Phone", users.Phone),
                };

                foreach (var (fieldName, value) in fieldsToValidate)
                {
                    /* Bloco de repetição responsável por chamar o método FieldExists que validará os campos e evitará a duplicação de registros.*/
                    if (await FieldExists(fieldName, value))
                    {
                        ModelState.AddModelError(fieldName, $"O {fieldName.ToLower()} informado já está em uso."); // Retorna a mensagem de erro ao usuário para cada campo que não tenha cumprido as exigências.
                    }
                }

                if (ModelState.ErrorCount > 0)
                {
                    return View(users); // Exibe a view com as mensagens de erro de cada campo preenchido de forma indevida.
                }

                if (!CheckPassword(users.Password, confirmPass))
                {
                    return View();
                }

                /* Assim que todas as validações forem realizadas e os dados estiver de acordo com as exigências o usuário chama o método que registra o usuário no banco de dados e exibe uma mensagem de feedback informando o sucesso da operação. Logo após o usuário é redirecionado para a página de listagem de usuários cadastrados.*/

                await _usersServices.RegisterUserAdministrator(users);
                TempData["SuccessMessage"] = "Usuário cadastrado com sucesso.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                TempData["ErrorPass"] = "Confirme a senha.";
                return View();
            }

            return View(users);
        }
        catch (Exception e)
        {
            // Caso um erro seja encontrado no processo uma excessão é lançada e um objeto de log armazenará a mensagem de erro obtida.
            _logger.LogError("Não foi possível cadsatrar o usuário", e.Message);
            return RedirectToAction("Index");
        }
    }

    public IActionResult Edit(int id)
    {
        /* Exibe  a view de edição de usuários com os dados recurados do usuário a ser editado de acordo com o Id do mesmo. */

        UserAdministrator users = _usersServices.SearchForId(id);
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ModifyAdministrator modifyAdministrator)
    {
        /* ActionResult Responsável por realizar o post dos dados de usuário a serem atualizados. Assim como no registro de usuários o método é envolvido em um bloco try catch para tratar excessões e fas uma validação do modelo de dados a fim de verificar se estão preenchidos corretamente. */
        try
        {
            UserAdministrator users = null;

            if (ModelState.IsValid)
            {
                users = new UserAdministrator()
                {
                    Id = modifyAdministrator.Id,
                    Masp = modifyAdministrator.Masp,
                    Name = modifyAdministrator.Name,
                    Login = modifyAdministrator.Login,
                    Email = modifyAdministrator.Email,
                    Phone = modifyAdministrator.Phone,
                    Position = modifyAdministrator.Position,
                };

                await _usersServices.AdministratorsEdit(users); // Chamada do método que realiza a edição de um usuário a pós as validações serem correspondidas
                TempData["SuccessMessage"] = "Usuário editado com sucesso.";
                return RedirectToAction("Index");
            }

            return View(users);
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível editar o usuário.";
            _logger.LogError("Não foi possível editar o usuário", e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Delete(string decision, UserAdministrator users)
    {

        try
        {
            if (decision == "delete")
            {
                if (users.Id != 0)
                {
                    _usersServices.DeleteUserAdministrator(users.Id);
                    TempData["SuccessMessage"] = "Usuário excluído com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir o usuário.";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível excluir o usuário.";
            _logger.LogError("Não foi possível excluir o usuário", e.Message);
            return RedirectToAction("Index");
        }

    }

    private async Task<bool> FieldExists(string fieldName, object value)
    {
        return await _database.Administrator.AnyAsync(u => EF.Property<object>(u, fieldName) == value);
    }

    private bool CheckPassword(string pass, string confirmPass)
    {
        if (confirmPass != pass)
        {
            TempData["ErrorPass"] = "As senhas são diferentes.";
            return false;
        }

        return true;
    }

}
