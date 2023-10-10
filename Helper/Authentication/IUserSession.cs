using SEP_Web.Models;

namespace SEP_Web.Helper.Authentication;
public interface IUserSession
{
    void UserCheckIn(UserAdministrator users);
    void UserCheckOut();
    UserAdministrator SearchUserSession();
    Task<UserAdministrator> UserSignIn(int? masp, string login);
}
