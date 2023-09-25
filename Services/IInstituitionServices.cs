using SEP_Web.Models;

namespace SEP_Web.Services;
public interface IInstituitionServices
{
    Instituition SearchForId(int id);
    Task<Instituition> RegisterInstituition(Instituition instituition);
    Task<ICollection<Instituition>> InstituitionsList();
    Task<Instituition> InstituitionEdit(Instituition instituition);
    void DeleteInstituition(int id);
}
