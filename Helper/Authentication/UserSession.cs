using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Helper.Authentication;
public class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly SEP_WebContext _database;

    public UserSession(IHttpContextAccessor httpContext, SEP_WebContext database)
    {
        _httpContext = httpContext;
        _database = database;
    }

    public Users SearchUserSession()
    {

        string userSession = _httpContext.HttpContext.Session.GetString("userCheckIn");

        if (string.IsNullOrEmpty(userSession)) return null;

        UserAdministrator administrator = JsonSerializer.Deserialize<UserAdministrator>(userSession);

        if (administrator == null)
        {
            UserEvaluator evaluator = JsonSerializer.Deserialize<UserEvaluator>(userSession);
            return evaluator;
        }

        return administrator;
    }

    public void UserCheckIn(Users users)
    {
        string value = JsonSerializer.Serialize(users); // Serializa um objeto de usuário e passa os dados obtidos em uma variável de sessão.
        _httpContext.HttpContext.Session.SetString("userCheckIn", value);
        _httpContext.HttpContext.Session.SetInt32("userType", (int)users.UserType);
    }
    
    public async Task<Users> UserSignIn(int? masp, string login) // Retorna um usuário que corresponda aos dados da busca.
    {
        Users administrator = await _database.Administrator.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper() || x.Masp == masp);

        if (administrator == null)
        {
            Users evaluator = await _database.Evaluator.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper() || x.Masp == masp);
            return evaluator;
        }

        return administrator;
    }

    public void UserCheckOut()
    {
        _httpContext.HttpContext.Session.Remove("userCheckIn");
    }
}