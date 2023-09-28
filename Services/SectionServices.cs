using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services;
public class SectionServices : ISectionServices
{

    private readonly SEP_WebContext _database;

    public SectionServices(SEP_WebContext database)
    {
        _database = database;
    }

    public async Task<Section> RegisterSection(Section section)
    {
        section.RegisterDate = DateTime.Now;
        await _database.Section.AddAsync(section);
        await _database.SaveChangesAsync();
        return section;
    }

    public async Task<Section> SectionEdit(Section section)
    {
        Section sectionnEdit = SearchForId(section.Id) ?? throw new Exception("Houve um erro na atualização da sessão");

        sectionnEdit.Name = section.Name;
        sectionnEdit.ModifyDate = DateTime.Now;

        sectionnEdit.UserAdministratorId = section.UserAdministratorId;
        sectionnEdit.DivisionId = section.DivisionId;

        _database.Section.Update(sectionnEdit);
        await _database.SaveChangesAsync();

        return sectionnEdit;
    }

    public async Task<ICollection<Section>> SectionsList()
    {
        ICollection<Section> sections = await _database.Section.ToListAsync();
        return sections;
    }

    public void DeleteSection(int id)
    {
        Section deleteSection = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão da sessão");

        _database.Section.Remove(deleteSection);
        _database.SaveChanges();
    }

    public Section SearchForId(int id)
    {
        return _database.Section.FirstOrDefault(x => x.Id == id);
    }

}
