using Microsoft.AspNetCore.Mvc;
using SEP_Web.Helper.Authentication;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class SectorController : Controller
{
    private readonly ILogger<SectorController> _logger;
    private readonly ISectorServices _sectorServices;
    private readonly IUserSession _session;

    public SectorController(ILogger<SectorController> logger, ISectorServices sectorServices, IUserSession session)
    {
        _logger = logger;
        _sectorServices = sectorServices;
        _session = session;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Sector> sectors = await _sectorServices.SectorsList();
        return View(sectors);
    }

    [HttpPost]
    public async Task<IActionResult> Register(Sector sector)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Users userInSession = await _session.SearchUserSession();
                sector.UserAdministratorId = userInSession.Id;

                await _sectorServices.RegisterSector(sector);
                TempData["SuccessMessage"] = "Setor cadastrado com sucesso.";
                return Json(new { stats = "OK" });
            }

            return Json(new { stats = "ERROR", message = "Não foi possível cadsatrar o setor!" });
        }
        catch (Exception e)
        {
            _logger.LogError("Não foi possível cadsatrar o setor", e.Message);
            return Json(new { stats = "INVALID", message = "Não foi possível cadsatrar o setor!" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Sector sector)
    {
        try
        {

            if (ModelState.IsValid)
            {
                Users userInSession = await _session.SearchUserSession();
                sector.UserAdministratorId = userInSession.Id;

                await _sectorServices.SectorEdit(sector);
                TempData["SuccessMessage"] = "Setor editado com sucesso.";
                return RedirectToAction("Index");
            }

            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível editar o setor.";
            _logger.LogError("Não foi possível editar o setor", e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Delete(string decision, Sector sector)
    {

        try
        {
            if (decision == "delete")
            {
                if (sector.Id != 0)
                {
                    _sectorServices.DeleteSector(sector.Id);
                    TempData["SuccessMessage"] = "Setor excluído com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir o setor.";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Não foi possível excluir o setor.";
            _logger.LogError("Não foi possível excluir o setor", e.Message);
            return RedirectToAction("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}