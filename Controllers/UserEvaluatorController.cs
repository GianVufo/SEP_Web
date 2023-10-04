using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Auth;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class UserEvaluatorController : Controller
{
    private readonly ILogger<UserEvaluatorController> _logger;
    private readonly IUserEvaluatorServices _evaluatorServices;
    private readonly IUserSession _session;
    private readonly SEP_WebContext _database;

    public UserEvaluatorController(ILogger<UserEvaluatorController> logger, IUserEvaluatorServices evaluatorServices, IUserSession session, SEP_WebContext database)
    {
        _logger = logger;
        _evaluatorServices = evaluatorServices;
        _database = database;
        _session = session;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<UserEvaluator> users = await _evaluatorServices.EvaluatorsList();
        return View(users);
    }

    public IActionResult Register()
    {
        List<Instituition> instituitions = _database.Instituition.ToList();
        if(instituitions != null)
        {
            ViewBag.Instituitions = new SelectList(instituitions, "Id", "Name");
        }
        
        return View();
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
                    if (await FieldExists(fieldName, value))
                    {
                        ModelState.AddModelError(fieldName, $"O {fieldName.ToLower()} informado já está em uso.");
                    }
                }

                if (ModelState.ErrorCount > 0)
                {
                    return View(evaluator);
                }

                if (!CheckPassword(evaluator.Password, confirmPass))
                {
                    return View();
                }

                await _evaluatorServices.RegisterUserEvaluator(evaluator);
                TempData["SuccessMessage"] = "Avaliador cadastrado com sucesso.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                TempData["ErrorPass"] = "Confirme a senha!";
                return View();
            }

            return View(evaluator);
        }
        catch (Exception e)
        {
            _logger.LogError("Não foi possível cadsatrar o avaliador", e.Message);
            return RedirectToAction("Index");
        }
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

    public IActionResult Edit(int id)
    {
        UserEvaluator evaluator = _evaluatorServices.SearchForId(id);
        return View(evaluator);
    }

    // [HttpPost]
    // public async Task<IActionResult> Edit(ModifyAdministrator modifyAdministrator)
    // {
    //     try
    //     {
    //         UserAdministrator users = null;

    //         if (ModelState.IsValid)
    //         {
    //             users = new UserAdministrator()
    //             {
    //                 Id = modifyAdministrator.Id,
    //                 Masp = modifyAdministrator.Masp,
    //                 Name = modifyAdministrator.Name,
    //                 Login = modifyAdministrator.Login,
    //                 Email = modifyAdministrator.Email,
    //                 Phone = modifyAdministrator.Phone,
    //                 Position = modifyAdministrator.Position,
    //             };

    //             await _usersServices.AdministratorsEdit(users); // Chamada do método que realiza a edição de um usuário a pós as validações serem correspondidas
    //             TempData["SuccessMessage"] = "Usuário editado com sucesso.";
    //             return RedirectToAction("Index");
    //         }

    //         return View(users);
    //     }
    //     catch (Exception e)
    //     {
    //         TempData["ErrorMessage"] = "Não foi possível editar o usuário.";
    //         _logger.LogError("Não foi possível editar o usuário", e.Message);
    //         return RedirectToAction("Index");
    //     }
    // }

    // [HttpPost]
    // public async Task<IActionResult> ChangeUserPassword(ChangePassword changePassword)
    // {
        
    //     try
    //     {
    //         UserAdministrator users = null;

    //         if (ModelState.IsValid)
    //         {
    //             if(changePassword.Password != changePassword.ComparePassword)
    //             {
    //                 return Json(new { stats = "INVALID" });
    //             }
            
    //             users = new UserAdministrator()
    //             {
    //                 Id = changePassword.Id,
    //                 Password = changePassword.Password
    //             };

    //             await _usersServices.ChangePassword(changePassword);
    //             TempData["SuccessMessage"] = "Senha editada com sucesso.";
    //             return Json(new { stats = "OK" });
    //         }

    //         return Json(new { stats = "ERROR"});
    //     }
    //     catch (Exception e)
    //     {
    //         TempData["ErrorMessage"] = "Não foi possível editar a senha.";
    //         _logger.LogError("Não foi possível editar a senha", e.Message);
    //         return Json(new { stats = "INVALID", message = "Não foi possível editar a senha!" });
    //     }
    // }

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

    private async Task<bool> FieldExists(string fieldName, object value)
    {
        return await _database.Evaluator.AnyAsync(u => EF.Property<object>(u, fieldName) == value);
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
