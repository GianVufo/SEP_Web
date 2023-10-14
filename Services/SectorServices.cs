using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services;
public class SectorServices : ISectorServices
{
    private readonly SEP_WebContext _database;

    public SectorServices(SEP_WebContext database)
    {
        _database = database;
    }

    public async Task<Sector> RegisterSector(Sector sector)
    {
        sector.RegisterDate = DateTime.Now;
        await _database.Sector.AddAsync(sector);
        await _database.SaveChangesAsync();
        return sector;
    }

    public async Task<ICollection<Sector>> SectorsList()
    {
        ICollection<Sector> sector = await _database.Sector.ToListAsync();
        return sector;
    }

    public async Task<ICollection<Sector>> GetSectorsAsync(int sectionId)
    {
        return await _database.Sector.Where(s => s.SectionId == sectionId).ToListAsync();
    }

    public async Task<Sector> SectorEdit(Sector sector)
    {
        Sector sectorEdit = SearchForId(sector.Id) ?? throw new Exception("Houve um erro na atualização do setor");

        sectorEdit.Name = sector.Name;
        sectorEdit.ModifyDate = DateTime.Now;

        sectorEdit.UserAdministratorId = sector.UserAdministratorId;
        sectorEdit.SectionId = sector.SectionId;

        _database.Sector.Update(sectorEdit);
        await _database.SaveChangesAsync();

        return sectorEdit;
    }

    public void DeleteSector(int id)
    {
        Sector deleteSector = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do setor");

        _database.Sector.Remove(deleteSector);
        _database.SaveChanges();
    }

    public Sector SearchForId(int id)
    {
        return _database.Sector.FirstOrDefault(x => x.Id == id);
    }
}
