using SEP_Web.Models;

namespace SEP_Web.Services;
public interface ISectorServices
{
    Sector SearchForId(int id);
    Task<Sector> RegisterSector(Sector sector);
    Task<ICollection<Sector>> SectorsList();
    Task<ICollection<Sector>> GetSectorsAsync(int sectionId);
    Task<Sector> SectorEdit(Sector sector);
    void DeleteSector(int id);
}
