using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEP_Web.Filters;
using SEP_Web.Helper.Validations;
using SEP_Web.Helper.Exceptions.Messages.FeedbackMessages;
using SEP_Web.Models;
using SEP_Web.Services;
using MySqlConnector;

namespace SEP_Web.Controllers;

[UserEvaluatorFilter]
public class UserEvaluatorController : Controller
{
    private readonly ILogger<UserEvaluatorController> _logger;
    private readonly IUserEvaluatorServices _evaluatorServices;
    private readonly IDivisionServices _divisionServices;
    private readonly ISectionServices _sectionServices;
    private readonly ISectorServices _sectorServices;
    private readonly IUserEvaluatorServices _usersServices;
    private readonly IValidationUsers _validation;

    public UserEvaluatorController(ILogger<UserEvaluatorController> logger, IUserEvaluatorServices evaluatorServices, IDivisionServices divisionServices, ISectorServices sectorServices, ISectionServices sectionServices, IUserEvaluatorServices usersServices, IValidationUsers validation)
    {
        _logger = logger;
        _evaluatorServices = evaluatorServices;
        _divisionServices = divisionServices;
        _sectionServices = sectionServices;
        _sectorServices = sectorServices;
        _usersServices = usersServices;
        _validation = validation;
    }
    
    public async Task<IActionResult> Index()
    {
        try
        {
            ICollection<UserEvaluator> users = await _evaluatorServices.EvaluatorsList();

            if(!(users == null || users.Count == 0)) return View(users);

            throw new ArgumentNullException(nameof(users), ExceptionsFeedbackMessage.ErrorEmptyCollection);

        }
        catch(MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            TempData["ErrorMessage"] = $"{ExceptionsFeedbackMessage.ErrorEvaluatorList} {ExceptionsFeedbackMessage.ErrorDatabaseConnection}"; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogError("{exceptionMessage} : {Message}, ErrorCode = {errorCode} - Represents {Error} ", ExceptionsFeedbackMessage.ErrorDatabaseConnection, ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("{exceptionMessage} : {Description} - ", ExceptionsFeedbackMessage.ErrorDetail, ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada de onde foram capturados os erros;

            return View(new List<UserEvaluator>());
        }
        catch(Exception ex2)
        {
            // GENERIC EXCEPTION :

            TempData["ErrorMessage"] = ExceptionsFeedbackMessage.ErrorEmptyCollection; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogWarning("{exceptionMessage} : {Message} value = '{InnerExeption}'", ExceptionsFeedbackMessage.ErrorEmptyCollection, ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o erro;

            _logger.LogWarning("{exceptionMessage} :  {Description}", ExceptionsFeedbackMessage.ErrorDetail, ex2.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;
            return View(new List<UserEvaluator>());
        }
        
    }

    public IActionResult Register()
    {
        /* HEAD */

        // ActionResult que exibe a página de cadastro de usuários que contém o formulário de registro com os campos necessários para um novo cadastro;

        // Responsável apenas por exibir a view que contém o formulário de cadastro;
        return View();
    }

    public IActionResult Edit(int id)
    {
        /* Exibe  a view de edição de usuários com os dados recuperados do usuário a ser editado de acordo com o Id do mesmo; */

        UserEvaluator users = _usersServices.SearchForId(id);
        return View(users); // monta a view alimentando-a com o objeto recuperado da busca;
    }

    [HttpGet]
    public async Task<IActionResult> GetDivisionsByInstituition(int instituitionId)
    {
        ICollection<Division> divisions = await _divisionServices.GetDivisionsAsync(instituitionId);

        var divisionList = divisions.Select(d => new SelectListItem
        {
            Text = d.Name,
            Value = d.Id.ToString(),
        });

        return Json(divisionList);
    }

    [HttpGet]
    public async Task<IActionResult> GetSectionsByDivisions(int divisionId)
    {
       ICollection<Section> sections = await _sectionServices.GetSectionsAsync(divisionId);

        var sectionList = sections.Select(d => new SelectListItem
        {
            Text = d.Name,
            Value = d.Id.ToString(),
        });

        return Json(sectionList);
    }

    [HttpGet]
    public async Task<IActionResult> GetSectorsBySections(int sectionId)
    {
       ICollection<Sector> sectors = await _sectorServices.GetSectorsAsync(sectionId);

        var sectorList = sectors.Select(d => new SelectListItem
        {
            Text = d.Name,
            Value = d.Id.ToString(),
        });

        return Json(sectorList);
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserEvaluator evaluator, string confirmPass)
    {
        /* HEAD */

        // ActionResult responsável por realizar o post das informações que serão recebidas através do formulário de cadastro de usuários avaliadores;

        // Recebe um objeto de "avaliador" de forma assíncrona que tem por objetivo ser incluído ao banco de dados como um novo registro, também recebe uma string "confirmPass" que será utilizada para realizar a comparação das senhas digitadas pelo usuário a fim de verificar se elas se correspondem;
            
        // O método está envolvido em um bloco try catch para lidar com as excessões geradas. Caso ocorra algum erro ou falha de um tratativa essencial uma excessão é lançada e devidamente tratada para trazer um retorno eficaz de acordo com a tratativa. O bloco ainda armazena os erros gerados em um arquivo de log para que facilite na manutenção e resolução dos erros;

        // Um usuário avaliador deve estar relacionado a um administrador no momento do seu cadastro;

        try{            

            if (ModelState.IsValid) // valida o modelo utilizado na requisição
            {
                /* Com o modelo validado uma variável é criada recebendo uma lista como atribuição. a lista contêm parâmetros que serão utilizados na validação dos dados do usuário a fim de evitar a duplicação de dados nos campos de preenchimento obrigatótio no registro de usuários administradores. */


                // Users userInSession = _session.SearchUserSession(); // Recuperando dados do administrador que irá cadastrar o avaliador;
                // evaluator.RegisteredBy = userInSession.Login; // Atibuindo o id do administrador que está realizando o cadastro do avaliador para preencher a chave estrangeira de relacionamento estre as entidades;

                // parâmetros a serem validados;
                var fieldsToValidate = new List<(string FieldName, object Value)>
                {
                    ("Masp", evaluator.Masp),
                    ("Name", evaluator.Name),
                    ("Login", evaluator.Login),
                    ("Email", evaluator.Email),
                    ("Phone", evaluator.Phone),
                };

                foreach (var (fieldName, value) in fieldsToValidate)
                {
                    /* Bloco de repetição pe responsável por chamar o método VerifyIfFieldExistsInBothUsersTable que validará os campos e evitará a duplicação de registros.*/

                    if (await _validation.VerifyIfFieldExistsInBothUsersTable(fieldName, value)) ModelState.AddModelError(fieldName, $"O {fieldName.ToLower()} informado já está em uso."); // Retorna a mensagem de erro ao usuário final para cada campo que não tenha cumprido as exigências.
                }

                if (ModelState.ErrorCount > 0) return View(evaluator); // Exibe a view com as respectivas mensagens de erro para cada campo preenchido de forma inválida.

                if (!_validation.ValidatePassword(evaluator.Password, confirmPass, this)) return View(); // Realiza a validação da senha que está sendo cadastrada comparando-a ao segundo campo de inserção de senha que valida a igualdade nas senhas digitadas a fim de evitar erros para o usuário na criação da senha;

                /* Assim que todos os dados forem validados de acordo com as exigências; */

                await _evaluatorServices.RegisterUserEvaluator(evaluator); // Utiliza o serviço correspondente ao usuário para relizar a nova inserção do registro;

                TempData["SuccessMessage"] = ExceptionsFeedbackMessage.SuccessEvaluatorRegistration; // Passa para um objeto TempData uma mensagem de sucesso que será exibida para o usuário caso tudo ocorra como o esperado;

                return RedirectToAction("Index"); // Redireciona à página principal de adminisradores que exibe a listagem completa dos mesmos;
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                /* Força o usuário a repetir a senha para que seja possível compará-las; */

                TempData["ErrorEvaluatorPass"] = "Confirme a senha!"; // TempData que retorna a mensagem de obrigatoriedade da confirmação de senha para o usuário final;

                return View();
            }

            return View(evaluator);
        }
        catch (MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            TempData["ErrorMessage"] = $"{ExceptionsFeedbackMessage.ErrorEvaluatorRegistration} {ExceptionsFeedbackMessage.ErrorDatabaseConnection}"; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogError("{exceptionMessage} : {Message}, ErrorCode = {errorCode} - Represents {Error} ", ExceptionsFeedbackMessage.ErrorDatabaseConnection, ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("{exceptionMessage} : {Description} - ", ExceptionsFeedbackMessage.ErrorDetail, ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada de onde foram capturados os erros;

            return View(evaluator); // Redireciona para a action Index para que usuário receba o feedback do erro;
        }
        catch (Exception ex2)
        {
            // GENERIC EXCEPTION :

            TempData["ErrorMessage"] = ExceptionsFeedbackMessage.ErrorEvaluatorRegistration; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogWarning("{exceptionMessage} : {Message} value = '{InnerExeption}'", ExceptionsFeedbackMessage.ErrorUnexpected, ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o erro;

            _logger.LogWarning("{exceptionMessage} : {Description}",  ExceptionsFeedbackMessage.ErrorDetail, ex2.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;

            return View(evaluator); // Redireciona para a action Index para que usuário receba o feedback do erro;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ModifyEvaluator modifyEvaluator)
    {
        /* ActionResult Responsável por realizar o post dos dados de usuário a serem atualizados. Assim como no registro de usuários o método é envolvido em um bloco try catch para tratar excessões e também realiza uma validação do modelo de dados a fim de verificar se os campos estão preenchidos corretamente e evita duplicatas. */
        try
        {
            /* Para validar campos duplicados sem que os dados do prórprio usuário a ser editado sejam vistos como uma duplicata um objeto do usuário já exitente armazena os dados do mesmo para que a validação verifique se campos inalterados pertencem apenas ao objeto recuperado; */
            UserEvaluator existingUser  = _usersServices.SearchForId(modifyEvaluator.Id);

            if (ModelState.IsValid)
            {

                var fieldsToValidate = new List<(string FieldName, object Value)>
                {
                    /* Com o modelo validado uma variável é criada recebendo uma lista como atribuição. a lista contêm parâmetros que serão utilizados na validação dos dados do usuário a fim de evitar a duplicação de dados nos campos de preenchimento obrigatótio no registro de usuários. */

                    // Parâmetros a serem validados;
                    ("Masp", modifyEvaluator.Masp),
                    ("Name", modifyEvaluator.Name),
                    ("Login", modifyEvaluator.Login),
                    ("Email", modifyEvaluator.Email),
                    ("Phone", modifyEvaluator.Phone),
                };

                foreach (var (fieldName, value) in fieldsToValidate)
                {
                    /* Bloco de repetição responsável por chamar o método VerifyIfFieldExistsInBothUsersTable que validará os campos e evitará a duplicação de registros.*/

                    if (_validation.IsFieldChanged(existingUser, fieldName, value)) 
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

                existingUser = new UserEvaluator() // Repassa as alterações realizadas para uma nova instância de administrador;
                {
                    Id = modifyEvaluator.Id,
                    Masp = modifyEvaluator.Masp,
                    Name = modifyEvaluator.Name,
                    Login = modifyEvaluator.Login,
                    Email = modifyEvaluator.Email,
                    Phone = modifyEvaluator.Phone,
                    InstituitionId = modifyEvaluator.InstituitionId,
                    DivisionId = modifyEvaluator.DivisionId,
                    SectionId = modifyEvaluator.SectionId,
                    SectorId = modifyEvaluator.SectorId,
                    Position = modifyEvaluator.Position,
                };

                await _usersServices.EvaluatorsEdit(existingUser); // Utiliza o serviço correspondente ao usuário para relizar a atualização do registro
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

    [UserAdminFilter]
    [HttpPost]
    public IActionResult Delete(string decision, UserEvaluator evaluator)
    {

        try
        {
            if (decision == "delete")
            {
                if (evaluator.Id != 0)
                {
                    _evaluatorServices.DeleteUserEvaluator(evaluator.Id);
                    TempData["SuccessMessage"] = "Avaliador excluído com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir o avaliador.";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível excluir o avaliador.";
            _logger.LogError("Não foi possível excluir o avaliador", e.Message);
            return RedirectToAction("Index");
        }

    }

}
