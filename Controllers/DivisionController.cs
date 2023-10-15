using Microsoft.AspNetCore.Mvc;
using SEP_Web.Helper.Authentication;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class DivisionController : Controller
{
    private readonly ILogger<DivisionController> _logger;
    private readonly IDivisionServices _divisionServices;
    private readonly IUserSession _session;

    public DivisionController(ILogger<DivisionController> logger, IDivisionServices divisionServices, IUserSession session)
    {
        _logger = logger;
        _divisionServices = divisionServices;
        _session = session;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Division> divisions = await _divisionServices.DivisionsList();
        return View(divisions);
    }

    [HttpPost]
    public async Task<IActionResult> Register(Division division)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Users userInSession = await _session.SearchUserSession();
                division.UserAdministratorId = userInSession.Id;

                await _divisionServices.RegisterDivision(division);
                TempData["SuccessMessage"] = "Divisão cadastrada com sucesso.";
                return Json(new { stats = "OK" });
            }

            return Json(new { stats = "ERROR", message = "Não foi possível cadsatrar a divisão!" });
        }
        catch (Exception e)
        {
            _logger.LogError("Não foi possível cadsatrar a divisão", e.Message);
            return Json(new { stats = "INVALID", message = "Não foi possível cadsatrar a divisão!" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Division division)
    {
        try
        {

            if (ModelState.IsValid)
            {
                Users userInSession = await _session.SearchUserSession();
                division.UserAdministratorId = userInSession.Id;

                await _divisionServices.DivisionEdit(division);
                TempData["SuccessMessage"] = "Divisão editada com sucesso.";
                return RedirectToAction("Index");
            }

            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível editar a divisão.";
            _logger.LogError("Não foi possível editar a divisão", e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Delete(string decision, Division division)
    {

        try
        {
            if (decision == "delete")
            {
                if (division.Id != 0)
                {
                    _divisionServices.DeleteDivision(division.Id);
                    TempData["SuccessMessage"] = "Divisão excluída com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir a divisão.";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível excluir a divisão.";
            _logger.LogError("Não foi possível excluir a divisão", e.Message);
            return RedirectToAction("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}