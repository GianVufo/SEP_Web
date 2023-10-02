using SEP_Web.Models;

namespace SEP_Web.Services;
public interface IUserAdministratorServices
{
    UserAdministrator SearchForId(int id);
    Task<UserAdministrator> RegisterUserAdministrator(UserAdministrator users);
    Task<ICollection<UserAdministrator>> AdministratorsList();
    Task<UserAdministrator> AdministratorsEdit(UserAdministrator users);
    Task<UserAdministrator> ChangePassword(ChangePassword changePassword);
    void DeleteUserAdministrator(int id);
}