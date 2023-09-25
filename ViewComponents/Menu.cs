using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SEP_Web.Models;

namespace SEP_Web.ViewComponents;
// Classe responsável por exibir o component Menu em /Shared/Components que contem o menu de navegação da aplicação.
public class Menu : ViewComponent //  A classe menu herda de View component
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        string userSession = HttpContext.Session.GetString("userCheckIn"); // Captura a variável de usuário armazenada na sessão.

        if (string.IsNullOrEmpty(userSession)) return null; // verifica de é nula ou vazia.

        MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(userSession));

        UserAdministrator users = await JsonSerializer.DeserializeAsync<UserAdministrator>(memoryStream); // os dados correspondentes são desserializados em um objto users utilizando o JsonSerializer

        return View(users); // Retorna o objeto dessesrializado.
    }

}