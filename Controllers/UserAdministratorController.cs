using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SEP_Web.Filters;
using SEP_Web.Helper.Validations;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter] // Filtro de permissão de acesso de usuário;
public class UserAdministratorController : Controller
{
    private readonly ILogger<UserAdministratorController> _logger;
    private readonly IUserAdministratorServices _usersServices;
    private readonly IValidationUsers _validation;

    public UserAdministratorController(ILogger<UserAdministratorController> logger, IUserAdministratorServices usersServices, IValidationUsers validation)
    {
        _logger = logger;
        _usersServices = usersServices;
        _validation = validation;
    }

    public async Task<IActionResult> Index()
    {
        /* Listagem geral de usuários cadastrados; */

        ICollection<UserAdministrator> users = await _usersServices.AdministratorsList(); // Armazena a listagem de usuários em uma coleção de administradores;
        return View(users); // Repassa o objeto de coleção para a view de exibição da listagem de usuários;
    }

    public IActionResult Register()
    {
        /* HEAD */

        // ActionResult que exibe a página de cadastro de usuários que contém o formulário de registro com os campos necessários para um novo cadastro;
        // Responsável apenas por exibir a view que contém o formulário de cadastro;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserAdministrator users, string confirmPass)
    {
        /* HEAD */

        // ActionResult responsável por realizar o post das informações que serão recebidas através do formulário de cadastro de usuários;

        // Recebe um objeto de "administrador" de forma assíncrona que tem por objetivo ser incluído ao banco de dados como um novo registro, também recebe uma string "confirmPass" que será utilizada para realizar a comparação das senhas digitadas pelo usuário a fim de verificar se elas se correspondem;
        
        // O método está envolvido em um bloco try catch para lidar com as excessões geradas. Caso ocorra algum erro ou falha de um tratativa essencial uma excessão é lançada e devidamente tratada para trazer um retorno eficaz de acordo com a tratativa. O bloco ainda armazena os erros gerados em um arquivo de log para que facilite na manutenção e resolução dos erros;
        
        try
        {
            if (ModelState.IsValid) // valida o modelo utilizado na requisição
            {
                var fieldsToValidate = new List<(string FieldName, object Value)>
                {
                    /* Com o modelo validado uma variável é criada recebendo uma lista como atribuição. a lista contêm parâmetros que serão utilizados na validação dos dados do usuário a fim de evitar a duplicação de dados nos campos de preenchimento obrigatótio no registro de usuários. */

                    // parâmetros a serem validados;
                    ("Masp", users.Masp),
                    ("Name", users.Name),
                    ("Login", users.Login),
                    ("Email", users.Email),
                    ("Phone", users.Phone),
                };

                foreach (var (fieldName, value) in fieldsToValidate)
                {
                    /* Bloco de repetição pe responsável por chamar o método VerifyIfFieldExistsInBothUsersTable que validará os campos e evitará a duplicação de registros.*/
                    
                    if (await _validation.VerifyIfFieldExistsInBothUsersTable(fieldName, value)) ModelState.AddModelError(fieldName, $"O {fieldName.ToLower()} informado já está em uso."); // Retorna a mensagem de erro ao usuário para cada campo que não tenha cumprido as exigências.
                }

                if (ModelState.ErrorCount > 0) return View(users); // Exibe a view com as respectivas mensagens de erro para cada campo preenchido de forma inválida.

                if (!_validation.ValidatePassword(users.Password, confirmPass, this)) return View();  // Realiza a validação da senha que está sendo cadastrada comparando-a ao segundo campo de inserção de senha que valida a igualdade nas senhas digitadas a fim de evitar erros para o usuário na criação da senha;

                /* Assim que todos os dados forem validados de acordo com as exigências; */
                await _usersServices.RegisterUserAdministrator(users); // Utiliza o serviço correspondente ao usuário para relizar a nova inserção do registro
                TempData["SuccessMessage"] = "Usuário cadastrado com sucesso."; // Passa para um objeto TempData uma mensagem de sucesso que será exibida para o usuário caso tudo ocorra como o esperado;
                return RedirectToAction("Index"); // Redireciona à página principal de adminisradores que exibe a listagem completa dos mesmos;
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                /* Força o usuário a repetir a senha para que seja possível compará-las; */

                TempData["ErrorPass"] = "Confirme a senha."; // TempData que retorna a mensagem de obrigatoriedade da confirmação de senha para o usuário final;
                return View();
            }

            return View(users);
        }
        catch (MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            TempData["ErrorMessage"] = "Não foi possível cadastrar o usuário. Erro ao tentar conectar com o banco de dados !"; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogError("[ADM_CONTROLLER] : Houve um erro na comunicação com o banco de dados impossibilitando o registro do administrador: {Message}, ErrorCode = {errorCode} - Represents {Error} ", ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("[ADM_CONTROLLER] :Detalhamento dos erros: {Description} - ", ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada de onde foram capturados os erros;

            return RedirectToAction("Index"); // Redireciona para a action Index para que usuário receba o feedback do erro;
        }
        catch (Exception ex2)
        {
            // GENERIC EXCEPTION :

            TempData["ErrorMessage"] = "Um erro inesperado não está permitindo que você cadastre o usuário, contate o desenvolvedor."; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogError("[ADM_CONTROLLER] : Houve um erro desconhecido tentar registrar o usuário administrador: {Message} value = '{InnerExeption}'", ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o erro;

            _logger.LogWarning("[ADM_SERVICE] : Objeto localizado {Description}", ex2.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;

            return RedirectToAction("Index"); // Redireciona para a action Index para que usuário receba o feedback do erro;
        }
    }

    public IActionResult Edit(int id)
    {
        /* Exibe  a view de edição de usuários com os dados recuperados do usuário a ser editado de acordo com o Id do mesmo; */

        UserAdministrator users = _usersServices.SearchForId(id);
        return View(users); // monta a view alimentando-a com o objeto recuperado da busca;
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ModifyAdministrator modifyAdministrator)
    {
        /* ActionResult Responsável por realizar o post dos dados de usuário a serem atualizados. Assim como no registro de usuários o método é envolvido em um bloco try catch para tratar excessões e também realiza uma validação do modelo de dados a fim de verificar se os campos estão preenchidos corretamente e evita duplicatas. */
        try
        {
            /* Para validar campos duplicados sem que os dados do prórprio usuário a ser editado sejam vistos como uma duplicata um objeto do usuário já exitente armazena os dados do mesmo para que a validação verifique se campos inalterados pertencem apenas ao objeto recuperado; */
            UserAdministrator existingUser  = _usersServices.SearchForId(modifyAdministrator.Id);

            if (ModelState.IsValid)
            {

                var fieldsToValidate = new List<(string FieldName, object Value)>
                {
                    /* Com o modelo validado uma variável é criada recebendo uma lista como atribuição. a lista contêm parâmetros que serão utilizados na validação dos dados do usuário a fim de evitar a duplicação de dados nos campos de preenchimento obrigatótio no registro de usuários. */

                    // Parâmetros a serem validados;
                    ("Masp", modifyAdministrator.Masp),
                    ("Name", modifyAdministrator.Name),
                    ("Login", modifyAdministrator.Login),
                    ("Email", modifyAdministrator.Email),
                    ("Phone", modifyAdministrator.Phone),
                };

                foreach (var (fieldName, value) in fieldsToValidate)
                {
                    /* Bloco de repetição responsável por chamar o método VerifyIfFieldExistsInBothUsersTable que validará os campos e evitará a duplicação de registros.*/

                    if (_usersServices.IsFieldChanged(existingUser, fieldName, value)) 
                    {
                        // verfica se campos inalterados não são uma duplicata de outro registro utilizando o objeto recuperado do usuário que está sendo atualizado;
                        if (await _validation.VerifyIfFieldExistsInBothUsersTable(fieldName, value))
                        {
                            ModelState.AddModelError(fieldName, $"O {fieldName.ToLower()} informado já está em uso."); // Retorna a mensagem de erro ao usuário para cada campo que não tenha cumprido as exigências;
                        }
                    }
                }

                if (ModelState.ErrorCount > 0)
                {
                    return View(existingUser); // Exibe a view com as respectivas mensagens de erro para cada campo preenchido de forma inválida;
                }

                existingUser = new UserAdministrator() // Repassa as alterações realizadas para uma nova instância de administrador;
                {
                    Id = modifyAdministrator.Id,
                    Masp = modifyAdministrator.Masp,
                    Name = modifyAdministrator.Name,
                    Login = modifyAdministrator.Login,
                    Email = modifyAdministrator.Email,
                    Phone = modifyAdministrator.Phone,
                    Position = modifyAdministrator.Position,
                };

                await _usersServices.AdministratorsEdit(existingUser); // Utiliza o serviço correspondente ao usuário para relizar a atualização do registro
                TempData["SuccessMessage"] = "Usuário editado com sucesso."; // Passa para um objeto TempData uma mensagem de sucesso que será exibida para o usuário caso tudo ocorra como o esperado;
                return RedirectToAction("Index"); // Redireciona à página principal de adminisradores que exibe a listagem completa dos mesmos;
            }

            return View(existingUser);
        }
        catch (Exception e)
        {
            // Caso alguma excessão seja lançada, aqui ela será tratada e o erro gerado será capturado pelo objeto de log do sistema para facilitar a depuração de falhas obtidas;
            TempData["ErrorMessage"] = "Não foi possível editar o usuário."; // TempData que armazena a mensagem de erro que será exibida ao usuário ao se deparar com uma falha na edição de um usuário;
            _logger.LogError("Não foi possível editar o usuário", e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUserPassword(ChangePassword changePassword)
    {
        /* TODO */

        // Corrigir as validações ao realizar a tentativa de alterar senha
        
        try
        {
            UserAdministrator users = null;

            if (ModelState.IsValid)
            {
                if(changePassword.Password != changePassword.ComparePassword)
                {
                    return Json(new { stats = "INVALID" });
                }
            
                users = new UserAdministrator()
                {
                    Id = changePassword.Id,
                    Password = changePassword.Password
                };

                await _usersServices.ChangePassword(changePassword);
                TempData["SuccessMessage"] = "Senha editada com sucesso.";
                return Json(new { stats = "OK" });
            }

            return Json(new { stats = "ERROR"});
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível editar a senha.";
            _logger.LogError("Não foi possível editar a senha", e.Message);
            return Json(new { stats = "INVALID", message = "Não foi possível editar a senha!" });
        }
    }

    [HttpPost]
    public IActionResult Delete(string decision, UserAdministrator users)
    {
        /* ActionResult que exclui o registro de um determinado usuário com base no parâmetro de decisão recebido na controller. O método também está envolvido em um bloco try/catch para tratar excessões que sejam geradas; */
        try
        {
            if (decision == "delete")
            {
                // A estrutura de decisão verifica o parâmetro de decisão recebido na controller;
                if (users.Id != 0)
                {
                    // Valida o id recebido para atestar se é válido;
                    _usersServices.DeleteUserAdministrator(users.Id); // Utiliza o serviço correspondente ao usuário para relizar a exclusão do registro;
                    TempData["SuccessMessage"] = "Usuário excluído com sucesso."; // TempData que armazena a mensagem de erro que será exibida ao usuário ao se deparar com uma falha na exclusão de um usuário;
                    return RedirectToAction("Index"); // Redireciona à página principal de adminisradores que exibe a listagem completa dos mesmos;
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
            // Caso alguma excessão seja lançada, aqui ela será tratada e o erro gerado será capturado pelo objeto de log do sistema para facilitar a depuração de falhas obtidas;
            TempData["ErrorMessage"] = "Não foi possível excluir o usuário.";
            _logger.LogError("Não foi possível excluir o usuário", e.Message); // Objeto de Log que armazena mensagens de erro geras pelo sistema;
            return RedirectToAction("Index");
        }

    }

}
