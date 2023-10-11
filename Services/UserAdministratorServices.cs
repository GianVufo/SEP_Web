using Microsoft.EntityFrameworkCore;
using SEP_Web.Helper.Authentication;
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
        /* HEAD */

        // Recebe um objeto de "administrador" de forma assíncrona que tem por objetivo ser incluído ao banco de dados como um novo registro;
        
        // O método está envolvido em um bloco try catch para lidar com as excessões geradas. Caso ocorra algum erro ou falha de um tratativa essencial uma excessão é lançada e devidamente tratada para trazer um retorno eficaz de acordo com a tratativa. O bloco ainda armazena os erros gerados em um arquivo de log para que facilite na manutenção e resolução dos erros;

        try{

            if(!(users == null)) // Valida se o usuário recebido como parâmetro não é nulo;
            {
                /* done */

                users.RegisterDate = DateTime.Now; // atribui ao atributo "Data de Registro" data e hora atuais no momento do registro de um administrador;

                users.Password = Cryptography.EncryptPassword(users.Password); // Criptografa com base em um algoritmo utilizando a biblioteca BCrypt a senha que está sendo cadastrada;

                await _database.Administrator.AddAsync(users); // Com base no contexto de dados concebido através de injeção de dependência, adiciona o novo registro como uma nova linha na tabela de administradores;

                await _database.SaveChangesAsync(); // Salva as alterações realizadas no contexto de dados;

                return users; // Retorna o objeto de usuários que é o tipo de retorno esperado pelo método;
            }
            
            /* fail */
            throw new ArgumentNullException(nameof(users), "[ADM_SERVICE] : Os dados fornecidos são inválidos para registrar um administrador.");

        }catch(MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("[ADM_SERVICE] : Houve um erro na comunicação com o banco de dados impossibilitando o registro do administrador: {Message}, ErrorCode = {errorCode} - Represents {Error} ", ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("[ADM_SERVICE] :Detalhamento dos erros: {Description} - ", ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;

            users = null; // Atrtibui um valor nullo ao objeto de administrador ;

            return users; // Retorna o objeto nullo; 
        }
        catch(Exception ex2)
        {
           // GENERIC EXCEPTION :

            _logger.LogWarning("ADM_[SERVICE] : Houve um erro desconhecido tentar registrar o usuário administrador: {Message} value = '{InnerExeption}'", ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o alerta;

            _logger.LogWarning("[ADM_SERVICE] : Objeto localizado {Description}", ex2.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;
            
            users = null; // Atrtibui um valor nullo ao objeto de administrador ;

            return users; // Retorna o objeto nullo; 
        }
    }

    public async Task<ICollection<UserAdministrator>> AdministratorsList()
    {
        /* HEAD */

        // Obtem uma Colleção de "Avaliadores" de forma assíncrona e retorna uma lista dos registros encontrados na tabela do banco de dados;

        // O método está envolvido em um bloco try catch para lidar com as excessões geradas. Caso ocorra algum erro ou falha de um tratativa essencial uma excessão é lançada e devidamente tratada para trazer um retorno eficaz de acordo com a tratativa. O bloco ainda armazena os erros gerados em um arquivo de log para que facilite na manutenção e resolução dos erros;
        
        try
        {
            ICollection<UserAdministrator> users = await _database.Administrator.ToListAsync(); // Coleção de administradores gerada de uma lista assíncrona da tabela de administradores presente no banco de dados. Este será o tipo de retorno final esperado pelo método;

            /* done */
            if (!(users == null || users.Count == 0)) return users; // Verifica se a coleção não é nula ou se não está vazia;

            /* fail */ 
            _logger.LogWarning("[ADM_SERVICE] : A coleção que está sendo acessada está vazia e não possui nenhum registro !"); // Armazena em um arquivo de log de avisos uma mensagem que informa que a lista está vazia;

            /* fail */ 
            throw new ArgumentNullException(nameof(users), "[ADM_SERVICE] : A coleção que está sendo acessada está vazia e não possui nenhum registro !"); // Lança uma excessão que também armazena uma mensagem que avisa sobre a lista estar vazia;

        }
        catch (MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("[ADM_SERVICE] : Houve um erro na comunicação com o banco de dados. Não é possível retornar a lista de administradores: {Message}, ErrorCode = {errorCode} - Represents {Error} ", ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("[ADM_SERVICE] : Detalhamento dos erros: {Description} - ", ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada de onde foram capturados os erros;
            
            return new List<UserAdministrator>(); // Retorna uma lista de administradores vazia que permite ao usuário final obter uma tela de retorno para não lidar com a exibição de excessões na tela;
        }
        catch (Exception ex2)
        {
            // GENERIC EXCEPTION :

            _logger.LogWarning("[ADM_SERVICE] : Houve um erro desconhecido ao retornar a lista de avaliadores: {Message} value = '{InnerExeption}'", ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o alerta;
            
            return new List<UserAdministrator>(); // Retorna uma lista de administradores vazia que permite ao usuário final obter uma tela de retorno para não lidar com a exibição de excessões na tela;
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