using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SEP_Web.Keys;
using SEP_Web.Models;

namespace SEP_Web.Filters;
public class UserAdminFilter : ActionFilterAttribute
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
            Users users = JsonSerializer.Deserialize<Users>(userSession);
            
            if (users == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }

            if (users.UserType != UserTypesEnum.User_Admin)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }
        }

        base.OnActionExecuted(context);
    }
}