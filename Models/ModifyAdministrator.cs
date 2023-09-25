using System.ComponentModel.DataAnnotations;

namespace SEP_Web.Models;
public class ModifyAdministrator
{
    [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo MASP é obrigatório.")]
    [Range(111, 999999, ErrorMessage = "Informe um MASP válido")]
    public int? Masp { get; set; }

    [Required(ErrorMessage = "Informe o nome completo.")]
    [MaxLength(50, ErrorMessage = "O nome não pode conter mais de 50 caracteres.")]
    [MinLength(10, ErrorMessage = "O nome não pode conter menos de 10 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Informe um nome de login!")]
    [MaxLength(35, ErrorMessage = "O login não pode conter mais de 35 caracteres.")]
    [MinLength(10, ErrorMessage = "O login não pode conter menos de 10 caracteres.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Informe um e-mail!")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe um telefone!")]
    [RegularExpression(@"^\d{2}\ \d{4}-\d{4}$", ErrorMessage = "O número de telefone é inválido. Tente: XX XXXX-XXXX")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Informe seu cargo!")]
    [MaxLength(35, ErrorMessage = "O cargo não pode conter mais de 35 caracteres.")]
    [MinLength(8, ErrorMessage = "O cargo não pode conter menos de 8 caracteres.")]
    public string Position { get; set; }

}
