using SEP_Web.Models;

namespace SEP_Web.Services;
public interface IUserAdministratorServices
{
    UserAdministrator SearchForId(int id); // Busca um usuário pelo Id;
    Task<UserAdministrator> RegisterUserAdministrator(UserAdministrator users); // Gera um novo registro de usuário administrador;
    Task<ICollection<UserAdministrator>> AdministratorsList(); // Coleção de administradores;
    Task<UserAdministrator> AdministratorsEdit(UserAdministrator users); // Edição de um registro já existente de um usuário administrador;
    Task<UserAdministrator> ChangePassword(ChangePassword changePassword); // Alterar senha de um usuário;
    bool IsFieldChanged(UserAdministrator existingUser, string fieldName, object newValue); // valida os campos de um usuário a fim de verificar duplicação de dados na edição de um usuário;
    void DeleteUserAdministrator(int id); // Exclui usuário administrador;
}