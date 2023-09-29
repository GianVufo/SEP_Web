using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;

namespace SEP_Web.Models;

[Table("Divisions")]
public class Division
{

    [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe uma descrição para a divisão!")]
    [StringLength(60), MaxLength(60, ErrorMessage = "A descrição não pode conter mais de 60 caracteres.")]
    [MinLength(8, ErrorMessage = "A descrição não pode conter menos de 8 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "informe uma data de registro!")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RegisterDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? ModifyDate { get; set; }

    [ForeignKey("InstituitionId"), Required(ErrorMessage = "Selecione um órgão!")]
    public int? InstituitionId { get; set; }
    public Instituition Instituition { get; set; }
    
    [ForeignKey("UserAdministratorId")]
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }

    public async Task<ICollection<Instituition>> InstituitionsList()
    {
        using SEP_WebContext _database = new();
        
        ICollection<Instituition> lista = await _database.Instituition.ToListAsync();
        return lista;
    }

    public async Task<string> InstituitionName(Division divisionId)
    {
        using SEP_WebContext _database = new();

        Instituition instituition = await _database.Instituition.Where(x => divisionId.InstituitionId == x.Id).FirstOrDefaultAsync();
        return instituition.Name.ToUpper();
    }
}
