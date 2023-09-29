using Microsoft.AspNetCore.Mvc;
using SEP_Web.Auth;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class SectionController : Controller
{
    private readonly ILogger<SectionController> _logger;
    private readonly SEP_WebContext _database;
    private readonly ISectionServices _sectionServices;
    private readonly IUserSession _session;

    public SectionController(ILogger<SectionController> logger, ISectionServices sectionServices, IUserSession session, SEP_WebContext database)
    {
        _logger = logger;
        _sectionServices = sectionServices;
        _session = session;
        _database = database;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Section> divisions = await _sectionServices.SectionsList();
        return View(divisions);
    }

    [HttpPost]
    public async Task<IActionResult> Register(Section sections)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UserAdministrator userInSession = _session.SearchUserSession();
                sections.UserAdministratorId = userInSession.Id;

                await _sectionServices.RegisterSection(sections);
                TempData["SuccessMessage"] = "Sessão cadastrada com sucesso.";
                return Json(new { stats = "OK" });
            }

            return Json(new { stats = "ERROR", message = "Não foi possível cadsatrar a sessão!" });
        }
        catch (Exception e)
        {
            _logger.LogError("Não foi possível cadsatrar a sessaõ", e.Message);
            return Json(new { stats = "INVALID", message = "Não foi possível cadsatrar a sessão!" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Section section)
    {
        
        /*TODO*/
        // Validar as entradas e saídas do modelo de exição e 
        // tratar excessões que são causadas pelo envio de
        // formulário com campos vazios.
        
        try
        {

            if (ModelState.IsValid)
            {
                UserAdministrator userInSession = _session.SearchUserSession();
                section.UserAdministratorId = userInSession.Id;

                await _sectionServices.SectionEdit(section);
                TempData["SuccessMessage"] = "Sessão editada com sucesso.";
                return RedirectToAction("Index");
            }

            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível editar a sessão.";
            _logger.LogError("Não foi possível editar a sessão", e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Delete(string decision, Section section)
    {

        try
        {
            if (decision == "delete")
            {
                if (section.Id != 0)
                {
                    _sectionServices.DeleteSection(section.Id);
                    TempData["SuccessMessage"] = "Sessão excluída com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir a sessão.";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível excluir a sessão.";
            _logger.LogError("Não foi possível excluir a sessão", e.Message);
            return RedirectToAction("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
