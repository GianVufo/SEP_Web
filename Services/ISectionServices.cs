using SEP_Web.Models;

namespace SEP_Web.Services;
public interface ISectionServices
{
    Section SearchForId(int id);
    Task<Section> RegisterSection(Section section);
    Task<ICollection<Section>> SectionsList();
    Task<ICollection<Section>> GetSectionsAsync(int DivisionId);
    Task<Section> SectionEdit(Section section);
    void DeleteSection(int id);
}
