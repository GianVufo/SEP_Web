using Microsoft.EntityFrameworkCore;
using SEP_Web.Helper.Authentication;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services;
public class UserEvaluatorServices : IUserEvaluatorServices
{
    private readonly SEP_WebContext _database;

    public UserEvaluatorServices(SEP_WebContext database)
    {
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
        ICollection<UserEvaluator> evaluator = await _database.Evaluator.ToListAsync();
        return evaluator;
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