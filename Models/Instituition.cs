using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP_Web.Models;

[Table("Instituitions")]
public class Instituition
{
    [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe uma descrição para o órgão!")]
    [MaxLength(60, ErrorMessage = "A descrição não pode conter mais de 60 caracteres.")]
    [MinLength(8, ErrorMessage = "A descrição não pode conter menos de 8 caracteres.")]
    public string Name { get; set; }

    [ForeignKey("UserAdministratorId")]
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }
}
