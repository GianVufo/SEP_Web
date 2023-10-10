using Microsoft.EntityFrameworkCore;
using SEP_Web.Helper.Authentication;
using SEP_Web.Helper.Exceptions;
using SEP_Web.Database;
using SEP_Web.Models;
using MySqlConnector;

namespace SEP_Web.Services;
public class UserAdministratorServices : IUserAdministratorServices
{
    private readonly ILogger<IUserAdministratorServices> _logger;
    private readonly SEP_WebContext _database;

    public UserAdministratorServices(SEP_WebContext database, ILogger<IUserAdministratorServices> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<UserAdministrator> RegisterUserAdministrator(UserAdministrator users)
    {
        // Ao registrar um usuário ele deve receber a data correspondente ao momento de cadastro do mesmo, e também ter sua senha criptografada;
        try{

            if(users == null) throw new ArgumentNullException(nameof(users), "Os dados informados são inválidos para registrar um administrador. Por favor, corrija-os e tente novamente!");
                
            users.RegisterDate = DateTime.Now; // atribuição de data e hora atual no momento do registro de um usuário;
            users.Password = Cryptography.EncryptPassword(users.Password); // Criptografa com base em um algoritmo utilizando a biblioteca BCrypt a senha que está sendo cadastrada;

            await _database.Administrator.AddAsync(users); // Com base no contexto de dados concebido através de injeção de dependência, adiciona o novo registro como uma nova linha na tabela de administradores;

            await _database.SaveChangesAsync(); // Salva as alterações realizadas no contexto de dados;

        }catch(MySqlException ex)
        {
            _logger.LogError("Houve um erro na comunicação com o banco de dados impossibilitando o registro do administrador.", ex.Message);
            throw;
        }
        catch(Exception ex2)
        {
            _logger.LogError("Houve um erro desconhecido no serviço de usuário administrador impossibilitando o registro do administradores.", ex2.Message);
            throw;
        }

        return users;
    }

    public async Task<ICollection<UserAdministrator>> AdministratorsList()
    {
        // O método de listagem de administradores retorna uma coleção de administradores que podem ser iterados como uma lista;
        
        try
        {
            ICollection<UserAdministrator> users = await _database.Administrator.ToListAsync();

            if (users == null) throw new ArgumentNullException(nameof(users), "Erro gerado na atribuição de valores da coleção de administradores");

            return users;

        }
        catch (MySqlException ex)
        {
            _logger.LogError("Houve um erro na comunicação com o banco de dados para retornar a lista de administradores", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Houve um desconhecido para retornar a lista de administradores", ex.Message);
            throw;
        }  
    }

    public async Task<UserAdministrator> AdministratorsEdit(UserAdministrator users)
    {
        // Ao editar um usuário todos os seus atributos básicos poderão ser alterados exceto sua senha, que será alterada por outro método específico que cuidará apenas da alteração de senha;

        // Para a edição de um usuário será realizada uma busca de acordo com o Id, caso não exista um parâmetro de busca uma excessão é lançada; 
        UserAdministrator searchUser = SearchForId(users.Id) ?? throw new Exception("Houve um erro na atualização do usuário");

        // Atributos editáveis de um usuário administrador por meio do método comum de edição;
        searchUser.Masp = users.Masp;
        searchUser.Name = users.Name;
        searchUser.Login = users.Login;
        searchUser.Email = users.Email;
        searchUser.Phone = users.Phone;
        searchUser.Position = users.Position;
        searchUser.ModifyDate = DateTime.Now; // Atribuição da data e hora atuais do momento de edição do registro;

        // Com base no contexto injetado, realiza a ação de atualização do registro na base de dados e logo após salva as alterações no contexto;
        _database.Administrator.Update(searchUser);
        await _database.SaveChangesAsync();

        return searchUser;
    }

    public async Task<UserAdministrator> ChangePassword(ChangePassword changePassword)
    {
        // O método de alteração da senha de um usuário administrador também realiza a busca do usuário de acordo com o Id, caso não exista um parâmetro de busca uma excessão é lançada;
        UserAdministrator searchUser = (SearchForId(changePassword.Id)) ??  throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

        searchUser.Password = Cryptography.EncryptPassword(changePassword.Password); // Realiza a alteração da senha criptografando a nova senha com base no algoritmo de criptografia da classe Cryptography;
        searchUser.ModifyDate = DateTime.Now; // atribuição de data e hora atuais do momento da alteração de senha;

        // Realiza a alteração no contexto da base de dados e salva as alterações realizadas;
        _database.Administrator.Update(searchUser);
        await _database.SaveChangesAsync();

        return searchUser;
    }

    public bool IsFieldChanged(UserAdministrator existingUser, string fieldName, object newValue)
    {
        // Verifique se o valor dos campos em um usuário já existente é diferente do novo valor (considerando nulos);
        // Método de auxílio da edição de usuários administradores;
        var existingValue = existingUser.GetType().GetProperty(fieldName).GetValue(existingUser);
        return !Equals(existingValue, newValue);
    }

    public void DeleteUserAdministrator(int id)
    {
        // A exclusão de usuários também é realizada partindo da busca de um usuário pelo Id, caso não exista um parâmetro de busca correspondente uma excessão é lançada;
        UserAdministrator foundUser = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do usuário");

        _database.Administrator.Remove(foundUser); // Remove o registro do usuário do contexto de dados;
        _database.SaveChanges(); // Salva as alterações realizadas no contexto;

    }

    public UserAdministrator SearchForId(int id)
    {
        // Implementação do método que busca os usuários pelo número de identificação "Id";
        return _database.Administrator.FirstOrDefault(x => x.Id == id);
    }

}