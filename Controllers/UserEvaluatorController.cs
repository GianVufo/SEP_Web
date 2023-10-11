using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEP_Web.Helper.Authentication;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Helper.Validations;
using SEP_Web.Models;
using SEP_Web.Services;
using MySqlConnector;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class UserEvaluatorController : Controller
{
    private readonly ILogger<UserEvaluatorController> _logger;
    private readonly IUserEvaluatorServices _evaluatorServices;
    private readonly IValidationUsers _validation;
    private readonly IUserSession _session;
    private readonly SEP_WebContext _database;

    public UserEvaluatorController(ILogger<UserEvaluatorController> logger, IUserEvaluatorServices evaluatorServices, IValidationUsers validation, IUserSession session, SEP_WebContext database)
    {
        _logger = logger;
        _evaluatorServices = evaluatorServices;
        _database = database;
        _session = session;
        _validation = validation;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            ICollection<UserEvaluator> users = await _evaluatorServices.EvaluatorsList();

            if(!(users == null || users.Count == 0)) return View(users);

            throw new ArgumentNullException(nameof(users), "[EVALUATOR_CONTROLLER] : A Coleção que está sendo acessada esta vazia ou é nula;");

        }
        catch(MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            TempData["ErrorMessage"] = "Não foi possível obter a listagem de avaliadores. Erro ao tentar conectar com o banco de dados !"; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogError("[EVALUATOR_CONTROLLER] : Houve um erro na comunicação com o banco de dados impossibilitando a listagem de avaliadores: {Message}, ErrorCode = {errorCode} - Represents {Error} ", ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("[EVALUATOR_CONTROLLER] :Detalhamento dos erros: {Description} - ", ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada de onde foram capturados os erros;

            return View(new List<UserEvaluator>());
        }
        catch(Exception ex2)
        {
            // GENERIC EXCEPTION :

            TempData["ErrorMessage"] = "Não existem avaliadores cadastrados !"; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogWarning("[EVALUATOR_CONTROLLER] : A lista de avaliadores se encontra vazia: {Message} value = '{InnerExeption}'", ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o erro;

            _logger.LogWarning("[EVALUATOR_CONTROLLER] : Objeto localizado {Description}", ex2.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;
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

    [HttpGet]
    public IActionResult GetDivisionsByInstituition(int instituitionId)
    {
        var divisions = _database.Division.Where(d => d.InstituitionId == instituitionId).ToList();

        var divisionList = divisions.Select(d => new SelectListItem
        {
            Text = d.Name,
            Value = d.Id.ToString(),
        });

        return Json(divisionList);
    }

    [HttpGet]
    public IActionResult GetSectionsByDivisions(int DivisionId)
    {
        var sections = _database.Section.Where(d => d.DivisionId == DivisionId).ToList();

        var sectionList = sections.Select(d => new SelectListItem
        {
            Text = d.Name,
            Value = d.Id.ToString(),
        });

        return Json(sectionList);
    }

    [HttpGet]
    public IActionResult GetSectorsBySections(int SectionId)
    {
        var sectors = _database.Sector.Where(d => d.SectionId == SectionId).ToList();

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


                UserAdministrator userInSession = _session.SearchUserSession(); // Recuperando dados do administrador que irá cadastrar o avaliador;

                evaluator.UserAdministratorId = userInSession.Id; // Atibuindo o id do administrador que está realizando o cadastro do avaliador para preencher a chave estrangeira de relacionamento estre as entidades;

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

                TempData["SuccessMessage"] = "Avaliador cadastrado com sucesso."; // Passa para um objeto TempData uma mensagem de sucesso que será exibida para o usuário caso tudo ocorra como o esperado;

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

            TempData["ErrorMessage"] = "Não foi possível cadastrar o usuário. Erro ao tentar conectar com o banco de dados !"; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogError("[EVALUATOR_CONTROLLER] : Houve um erro na comunicação com o banco de dados impossibilitando o registro do avaliador: {Message}, ErrorCode = {errorCode} - Represents {Error} ", ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("[EVALUATOR_CONTROLLER] :Detalhamento dos erros: {Description} - ", ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada de onde foram capturados os erros;

            return View(evaluator); // Redireciona para a action Index para que usuário receba o feedback do erro;
        }
        catch (Exception ex2)
        {
            // GENERIC EXCEPTION :

            TempData["ErrorMessage"] = "Um erro inesperado não está permitindo que você cadastre o usuário, contate o desenvolvedor."; // Mensagem de retorno que será exibida ao usuário final informando o erro ocorrido;

            _logger.LogWarning("[EVALUATOR_CONTROLLER] : Houve um erro desconhecido tentar registrar o usuário avaliador: {Message} value = '{InnerExeption}'", ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o erro;

            _logger.LogWarning("[EVALUATOR_CONTROLLER] : Objeto localizado {Description}", ex2.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;

            return View(evaluator); // Redireciona para a action Index para que usuário receba o feedback do erro;
        }
    }

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
