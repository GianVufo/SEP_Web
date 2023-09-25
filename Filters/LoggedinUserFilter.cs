using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SEP_Web.Models;

namespace SEP_Web.Filters;
public class LoggedinUserFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {

        string userSession = context.HttpContext.Session.GetString("userCheckIn");

        if (string.IsNullOrEmpty(userSession))
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        }
        else
        {
            UserAdministrator users = JsonSerializer.Deserialize<UserAdministrator>(userSession);
            if (users == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
        }

        base.OnActionExecuted(context);
    }
}
