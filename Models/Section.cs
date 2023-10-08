using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;

namespace SEP_Web.Models;

[Table("Sections")]
public class Section
{
    /* TO DO */
    [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe uma descrição para a sessão!")]
    [StringLength(60), MaxLength(60, ErrorMessage = "A sessão não pode conter mais de 60 caracteres.")]
    [MinLength(8, ErrorMessage = "A sessão não pode conter menos de 8 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "informe uma data de registro!")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RegisterDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? ModifyDate { get; set; }

    [ForeignKey("DivisionId"), Required(ErrorMessage = "Selecione uma divisão!")]
    public int DivisionId { get; set; }
    public Division Division { get; set; }

    [ForeignKey("UserAdministratorId")]
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }

    public async Task<string> DivisionName(Section sectionId)
    {
        using SEP_WebContext _database = new();

        Division division = await _database.Division.Where(x => sectionId.DivisionId == x.Id).FirstOrDefaultAsync();
        return division.Name.ToUpper();
    }
}
