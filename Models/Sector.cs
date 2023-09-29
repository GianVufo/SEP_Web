using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;

namespace SEP_Web.Models;
[Table("Sectors")]
public class Sector
{
    /* TO DO */
    [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe uma descrição para o setor!")]
    [StringLength(60), MaxLength(60, ErrorMessage = "O setor não pode conter mais de 60 caracteres.")]
    [MinLength(8, ErrorMessage = "O setor não pode conter menos de 8 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "informe uma data de registro!")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RegisterDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? ModifyDate { get; set; }

    [ForeignKey("SectionId"), Required(ErrorMessage = "Selecione uma sessão!")]
    public int SectionId { get; set; }
    public Section Section { get; set; }

    [ForeignKey("UserAdministratorId")]
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }

    public async Task<ICollection<Section>> SectionList()
    {
        using SEP_WebContext _database = new();
        
        ICollection<Section> lista = await _database.Section.ToListAsync();
        return lista;
    }

    public async Task<string> SectionName(Sector sectorId)
    {
        using SEP_WebContext _database = new();

        Section section = await _database.Section.Where(x => sectorId.SectionId == x.Id).FirstOrDefaultAsync();
        return section.Name;
    }
}
