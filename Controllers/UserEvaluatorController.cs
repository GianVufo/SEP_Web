using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Auth;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Helper.Validations;
using SEP_Web.Models;
using SEP_Web.Services;

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
        ICollection<UserEvaluator> users = await _evaluatorServices.EvaluatorsList();
        return View(users);
    }

    public IActionResult Register()
    {
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

        try{
            if (ModelState.IsValid) // valida o modelo utilizado na requisição
            {
                UserAdministrator userInSession = _session.SearchUserSession();
                evaluator.UserAdministratorId = userInSession.Id;

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
                    if (await _validation.VerifyIfFieldExistsInBothUsersTable(fieldName, value))
                    {
                        ModelState.AddModelError(fieldName, $"O {fieldName.ToLower()} informado já está em uso.");
                    }
                }

                if (ModelState.ErrorCount > 0)
                {
                    return View(evaluator);
                }

                if (!_validation.ValidatePassword(evaluator.Password, confirmPass, this))
                {
                    return View();
                }

                await _evaluatorServices.RegisterUserEvaluator(evaluator);
                TempData["SuccessMessage"] = "Avaliador cadastrado com sucesso.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                TempData["ErrorEvaluatorPass"] = "Confirme a senha!";
                return View();
            }

            return View();
        }
        catch (Exception e)
        {
            _logger.LogError("Não foi possível cadsatrar o avaliador", e.Message);
            return RedirectToAction("Index");
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
