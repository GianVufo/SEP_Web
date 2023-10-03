using SEP_Web.Models;

namespace SEP_Web.Services;
public interface IUserEvaluatorServices
{
    UserEvaluator SearchForId(int id);
    Task<UserEvaluator> RegisterUserEvaluator(UserEvaluator evaluator);
    Task<ICollection<UserEvaluator>> EvaluatorsList();
    Task<UserEvaluator> EvaluatorsEdit(UserEvaluator evaluator);
    void DeleteUserEvaluator(int id);
}