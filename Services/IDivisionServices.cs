using SEP_Web.Models;

namespace SEP_Web.Services;
public interface IDivisionServices
{
    Division SearchForId(int id);
    Task<Division> RegisterDivision(Division division);
    Task<ICollection<Division>> DivisionsList();
    Task<ICollection<Division>> GetDivisionsAsync(int instituitionId);
    Task<Division> DivisionEdit(Division division);
    void DeleteDivision(int id);
}
