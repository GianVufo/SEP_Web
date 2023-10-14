using SEP_Web.Models;

namespace SEP_Web.Helper.Authentication;
public interface IUserSession
{
    void UserCheckIn(Users users);
    void UserCheckOut();
    Users SearchUserSession();
    Task<Users> UserSignIn(int? masp, string login);
}
