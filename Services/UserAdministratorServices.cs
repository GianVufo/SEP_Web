using Microsoft.EntityFrameworkCore;
using SEP_Web.Auth;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services;
public class UserAdministratorServices : IUserAdministratorServices
{
    private readonly SEP_WebContext _database;

    public UserAdministratorServices(SEP_WebContext database)
    {
        _database = database;
    }

    public async Task<UserAdministrator> RegisterUserAdministrator(UserAdministrator users)
    {
        users.RegisterDate = DateTime.Now;
        users.Password = Cryptography.EncryptPassword(users.Password);
        await _database.Administrator.AddAsync(users);
        await _database.SaveChangesAsync();
        return users;
    }

    public async Task<ICollection<UserAdministrator>> AdministratorsList()
    {
        ICollection<UserAdministrator> users = await _database.Administrator.ToListAsync();
        return users;
    }

    public async Task<UserAdministrator> AdministratorsEdit(UserAdministrator users)
    {
        UserAdministrator searchUser = SearchForId(users.Id) ?? throw new Exception("Houve um erro na atualização do usuário");

        searchUser.Masp = users.Masp;
        searchUser.Name = users.Name;
        searchUser.Login = users.Login;
        searchUser.Email = users.Email;
        searchUser.Phone = users.Phone;
        searchUser.Position = users.Position;
        searchUser.ModifyDate = DateTime.Now;

        _database.Administrator.Update(searchUser);
        await _database.SaveChangesAsync();

        return searchUser;
    }

    public async Task<UserAdministrator> ChangePassword(ChangePassword changePassword)
    {
        UserAdministrator searchUser = (SearchForId(changePassword.Id)) ??  throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

        searchUser.Password = Cryptography.EncryptPassword(changePassword.Password);
        searchUser.ModifyDate = DateTime.Now;

        _database.Administrator.Update(searchUser);
        await _database.SaveChangesAsync();

        return searchUser;
    }

    public void DeleteUserAdministrator(int id)
    {
        UserAdministrator foundUser = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do usuário");

        _database.Administrator.Remove(foundUser);
        _database.SaveChanges();

    }

    public UserAdministrator SearchForId(int id)
    {
        return _database.Administrator.FirstOrDefault(x => x.Id == id);
    }

}