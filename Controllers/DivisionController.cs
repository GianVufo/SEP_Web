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
    private readonly IDivisionServices _divisionServices;

    public DivisionController(ILogger<DivisionController> logger, IDivisionServices divisionServices)
    {
        _logger = logger;
        _divisionServices = divisionServices;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Division> divisions = await _divisionServices.DivisionsList();
        return View(divisions);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}