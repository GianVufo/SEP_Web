using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Auth;
public class Session : IUserSession
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly SEP_WebContext _database;

    public Session(IHttpContextAccessor httpContext, SEP_WebContext database)
    {
        _httpContext = httpContext;
        _database = database;
    }

    public UserAdministrator SearchUserSession()
    {

        string userSession = _httpContext.HttpContext.Session.GetString("userCheckIn");

        if (string.IsNullOrEmpty(userSession)) return null;

        return JsonSerializer.Deserialize<UserAdministrator>(userSession);
    }

    public void UserCheckIn(UserAdministrator users)
    {
        string value = JsonSerializer.Serialize(users); // Serialiaza um objeto de usuário e passa os dados obtidos em uma variável de sessão.
        _httpContext.HttpContext.Session.SetString("userCheckIn", value);
    }
    
    public async Task<UserAdministrator> UserSignIn(int? masp, string login) // Retorna um usuário que corresponda aos dados da busca.
    {
        return await _database.Administrator.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper() || x.Masp == masp);
    }

    public void UserCheckOut()
    {
        _httpContext.HttpContext.Session.Remove("userCheckIn");
    }
}