using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Keys;
using SEP_Web.Models;

namespace SEP_Web.Helper.Authentication;

// Classe responsável por implementar a interface que traz o contrato dos métodos de sessão;

public class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly SEP_WebContext _database;

    public UserSession(IHttpContextAccessor httpContext, SEP_WebContext database)
    {
        _httpContext = httpContext;
        _database = database;
    }

    public async Task<Users> SearchUserSession()
    {

        string userSession = _httpContext.HttpContext.Session.GetString("userCheckIn"); // armazena em uma string a serialização do objeto que está armazenada na sessão;

        if (string.IsNullOrEmpty(userSession)) return null;
        
        Users user = new(); // Instância de um objeto de users

        // Utiliza uma condicional para verificar qual o tipo do usuário que está armazenado.
        if(userSession.Contains("UserType") == Convert.ToBoolean(UserTypesEnum.User_Admin))
        {
            // Retorna um objeto desserializado de acordo com o tipo que lhe corresponde;
            user = await JsonSerializer.DeserializeAsync<UserAdministrator>(new MemoryStream(Encoding.UTF8.GetBytes(userSession)));
        }
        else if(userSession.Contains("UserType") == Convert.ToBoolean(UserTypesEnum.User_Evaluator))
        {
            user = await JsonSerializer.DeserializeAsync<UserEvaluator>(new MemoryStream(Encoding.UTF8.GetBytes(userSession)));
        }

        return user;
    }

    public void UserCheckIn(Users users)
    {
        // UserCheckIn através do parâmetro recebido obtem uma serialização do objeto e seta os dados capturados em variáveis de sessão;

        // O método recebe como parâmetro um objeto de "Users", a string "value" recebe uma serialização Json do objeto de usuários;
        string value = JsonSerializer.Serialize(users);
        
        // Duas variáveis são setatadas na sessão contendo respectivamente, dados de um objeto de usuários que se encontra serializado e um int que corresponde ao tipo do usuário;
        _httpContext.HttpContext.Session.SetString("userCheckIn", value); // Objeto de users;
        _httpContext.HttpContext.Session.SetInt32("userType", (int)users.UserType); /// int tipo de usuário;
    }
    
    public async Task<Users> UserSignIn(int? masp, string login) // Retorna um usuário que corresponda aos dados da busca.
    {
        // UserSignIn recebe dois parâmetros para utilizá-los em uma pesquisa na base de dados em busca de um usuário que corresponda aos parâmetros passados e retorna um objeto correspondente;

        // Com base no int? masp e na string login, "administrator" recebe um objeto de usuário retornado da pesquisa Linq a base de dados. O usuário é buscado inicialmente na tabela de administradores;
        Users administrator = await _database.Administrator.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper() || x.Masp == masp);

        if (administrator == null)
        {
            // Caso um administrador não seja obtido, "evaluator" pesquisa na tabela de avaliadores por um usuário correspondente, caso encontrado o retorna;
            Users evaluator = await _database.Evaluator.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper() || x.Masp == masp);
            return evaluator;
        }

        return administrator; // retorno de administrador
    }

    public void UserCheckOut()
    {
        // Uiliza os métodos Remove() e Clear() para encerrar a sessão e limpar os dados nela armazenados;
        _httpContext.HttpContext.Session.Remove("userCheckIn");
        _httpContext.HttpContext.Session.Clear();
    }
}