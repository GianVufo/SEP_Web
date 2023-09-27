using Microsoft.AspNetCore.Mvc;
using SEP_Web.Auth;
using SEP_Web.Database;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers;

[UserAdminFilter]
public class DivisionController : Controller
{
    private readonly ILogger<DivisionController> _logger;
    private readonly SEP_WebContext _database;
    private readonly IDivisionServices _divisionServices;
    private readonly IUserSession _session;

    public DivisionController(ILogger<DivisionController> logger, IDivisionServices divisionServices, IUserSession session, SEP_WebContext database)
    {
        _logger = logger;
        _divisionServices = divisionServices;
        _session = session;
        _database = database;
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
                UserAdministrator userInSession = _session.SearchUserSession();
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}