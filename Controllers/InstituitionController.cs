using Microsoft.AspNetCore.Mvc;
using SEP_Web.Auth;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class InstituitionController : Controller
{
    private readonly ILogger<InstituitionController> _logger;
    private readonly IInstituitionServices _instituitionServices;
    private readonly IUserSession _session;
    private readonly SEP_WebContext _database;

    public InstituitionController(ILogger<InstituitionController> logger, IInstituitionServices instituitionServices, SEP_WebContext databse, IUserSession session)
    {
        _logger = logger;
        _instituitionServices = instituitionServices;
        _database = databse;
        _session = session;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Instituition> instituitions = await _instituitionServices.InstituitionsList();

        return View(instituitions);
    }

    [HttpPost]
    public async Task<IActionResult> Register(Instituition instituition)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UserAdministrator userInSession = _session.SearchUserSession();
                instituition.UserAdministratorId = userInSession.Id;

                await _instituitionServices.RegisterInstituition(instituition);
                TempData["SuccessMessage"] = "Órgão cadastrado com sucesso.";
                return Json(new { stats = "OK" });
            }

            return Json(new { stats = "ERROR", message = "Não foi possível cadsatrar o órgão!" });
        }
        catch (Exception e)
        {
            _logger.LogError("Não foi possível cadsatrar o órgão", e.Message);
            return Json(new { stats = "INVALID", message = "Não foi possível cadsatrar o órgão!" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Instituition instituition)
    {
        try
        {

            if (ModelState.IsValid)
            {
                UserAdministrator userInSession = _session.SearchUserSession();
                instituition.UserAdministratorId = userInSession.Id;

                await _instituitionServices.InstituitionEdit(instituition);
                TempData["SuccessMessage"] = "órgão editado com sucesso.";
                return RedirectToAction("Index");
            }

            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível editar o órgão.";
            _logger.LogError("Não foi possível editar o usuário", e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Delete(string decision, Instituition instituition)
    {

        try
        {
            if (decision == "delete")
            {
                if (instituition.Id != 0)
                {
                    _instituitionServices.DeleteInstituition(instituition.Id);
                    TempData["SuccessMessage"] = "Órgão excluído com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir o órgão.";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível excluir o órgão.";
            _logger.LogError("Não foi possível excluir o órgão", e.Message);
            return RedirectToAction("Index");
        }

    }

}
