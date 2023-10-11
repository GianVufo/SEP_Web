using Microsoft.EntityFrameworkCore;
using SEP_Web.Helper.Authentication;
using SEP_Web.Database;
using SEP_Web.Models;
using MySqlConnector;

namespace SEP_Web.Services;
public class UserEvaluatorServices : IUserEvaluatorServices
{
    private readonly ILogger<IUserEvaluatorServices> _logger;
    private readonly SEP_WebContext _database;

    public UserEvaluatorServices(SEP_WebContext database, ILogger<IUserEvaluatorServices> logger)
    {
        _logger = logger;
        _database = database;
    }

    public async Task<UserEvaluator> RegisterUserEvaluator(UserEvaluator evaluator)
    {
        evaluator.RegisterDate = DateTime.Now;
        evaluator.Password = Cryptography.EncryptPassword(evaluator.Password);
        await _database.Evaluator.AddAsync(evaluator);
        await _database.SaveChangesAsync();
        return evaluator;
    }

    public async Task<ICollection<UserEvaluator>> EvaluatorsList()
    {
        /* HEAD */

        // Obtem uma Colleção de "Avaliadores" de forma assíncrona e retorna uma lista dos registros encontrados na tabela do banco de dados;

        // O método está envolvido em um bloco try catch para lidar com as excessões geradas. Caso ocorra algum erro ou falha de um tratativa essencial uma excessão é lançada e devidamente tratada para trazer um retorno eficaz de acordo com a tratativa. O bloco ainda armazena os erros gerados em um arquivo de log para que facilite na manutenção e resolução dos erros;

        try
        {
            ICollection<UserEvaluator> evaluator = await _database.Evaluator.ToListAsync(); // Coleção de avaliadores gerada de uma lista assíncrona da tabela de avalidores presente no banco de dados. Este será o tipo de retorno final esperado pelo método;

            /* done */
            if (!(evaluator == null || evaluator.Count == 0)) return evaluator; // Verifica se a coleção não é nula ou se não está vazia;

            /* fail */
            _logger.LogWarning("A coleção que está sendo acessada está vazia e não possui nenhum registro !"); // Armazena em um arquivo de log de avisos uma mensagem que informa que a lista está vazia;
            
            /* fail */
            throw new ArgumentNullException(nameof(evaluator), "A coleção que está sendo acessada está vazia e não possui nenhum registro !"); // Lança uma excessão que também armazena uma mensagem que avisa sobre a lista estar vazia;

        }
        catch (MySqlException ex)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("Houve um erro na comunicação com o banco de dados. Não é possível retornar a lista de avaliadores: {Message}, ErrorCode = {errorCode} - Represents {Error} ", ex.Message.ToUpper(), ex.Number, ex.ErrorCode); // Armazena em um arquivo de log de erros uma mensagem personalizada seguida de informações sobre o erro capturado;

            _logger.LogError("Detalhamento dos erros: {Description} - ", ex.StackTrace.Trim()); // Armazena em um arquivo de log de errors a descrição detalhada e de onde foram capturados os erros;

            return new List<UserEvaluator>(); // Retorna uma lista de avaliadores vazia que permite ao usuário final obter uma tela de retorno para não lidar com a exibição de excessões na tela;
        }
        catch (Exception ex2)
        {
            // GENERIC EXCEPTION :

            _logger.LogWarning("Houve um erro desconhecido ao retornar a lista de avaliadores: {Message} value = '{InnerExeption}'", ex2.Message, ex2.InnerException); // Armazena em um arquivo de log de avisos uma mensagem personalizada seguida de informações sobre o alerta;

            _logger.LogWarning("A coleção está localizada {Description}", ex2.StackTrace.Trim());
            return new List<UserEvaluator>(); // Retorna uma lista de avaliadores vazia que permite ao usuário final obter uma tela de retorno para não lidar com a exibição de excessões na tela;
        }
    }

    public async Task<UserEvaluator> EvaluatorsEdit(UserEvaluator evaluator)
    {
        UserEvaluator searchEvaluator = SearchForId(evaluator.Id) ?? throw new Exception("Houve um erro na atualização do avaliador");

        searchEvaluator.Masp = evaluator.Masp;
        searchEvaluator.Name = evaluator.Name;
        searchEvaluator.Login = evaluator.Login;
        searchEvaluator.Email = evaluator.Email;
        searchEvaluator.Phone = evaluator.Phone;
        searchEvaluator.Position = evaluator.Position;
        searchEvaluator.ModifyDate = DateTime.Now;

        _database.Evaluator.Update(searchEvaluator);
        await _database.SaveChangesAsync();

        return searchEvaluator;
    }

    public void DeleteUserEvaluator(int id)
    {
        UserEvaluator foundEvaluator = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do avaliador");

        _database.Evaluator.Remove(foundEvaluator);
        _database.SaveChanges();

    }

    public UserEvaluator SearchForId(int id)
    {
        return _database.Evaluator.FirstOrDefault(x => x.Id == id);
    }

}