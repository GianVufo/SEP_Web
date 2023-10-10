using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SEP_Web.Helper.Authentication;
using SEP_Web.Keys;

namespace SEP_Web.Models;
[Table("Evaluators")]
public class UserEvaluator
{
    /* TO DO */

    [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo MASP é obrigatório!")]
    [Range(111, 999999, ErrorMessage = "Informe um MASP válido!")]
    public int? Masp { get; set; }

    [Required(ErrorMessage = "Informe o nome completo!")]
    [StringLength(50), MaxLength(50, ErrorMessage = "O nome não pode conter mais de 50 caracteres!")]
    [MinLength(10, ErrorMessage = "O nome não pode conter menos de 10 caracteres!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Informe um nome de login!")]
    [StringLength(35), MaxLength(35, ErrorMessage = "O login não pode conter mais de 35 caracteres!")]
    [MinLength(10, ErrorMessage = "O login não pode conter menos de 10 caracteres!")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Informe uma senha!")]
    [StringLength(35), MaxLength(150, ErrorMessage = "A senha pode conter no máximo 150 caracteres!")]
    [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres!")]
    public string Password { get; set; }

    [StringLength(100), Required(ErrorMessage = "Informe um e-mail!")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido!")]
    public string Email { get; set; }

    [StringLength(15), Required(ErrorMessage = "Informe um telefone!")]
    [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[0-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "O número de telefone é inválido. Tente: (XX) XXXXX-XXXX")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Informe seu cargo!")]
    [StringLength(35), MaxLength(35, ErrorMessage = "O cargo não pode conter mais de 35 caracteres!")]
    [MinLength(8, ErrorMessage = "O cargo não pode conter menos de 8 caracteres!")]
    public string Position { get; set; }

    [Required(ErrorMessage = "Informe o tipo de usuário!")]
    public UserTypesEnum? UserType { get; set; }

    [Required(ErrorMessage = "informe uma data de registro!")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RegisterDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? ModifyDate { get; set; }

    [ForeignKey("UserAdministratorId"), Required(ErrorMessage = "É necessário que um avaliador esteja atrelado a um administrador!")]
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }

    [ForeignKey("InstituitionId"), Required(ErrorMessage = "Selecione um órgão!")]
    public int? InstituitionId { get; set; }
    public Instituition Instituition { get; set; }

    [ForeignKey("DivisionId"), Required(ErrorMessage = "Selecione uma divisão!")]
    public int? DivisionId { get; set; }
    public Division Division { get; set; }

    [ForeignKey("SectionId"), Required(ErrorMessage = "Selecione uma seção!")]
    public int? SectionId { get; set; }
    public Section Section { get; set; }

    [ForeignKey("SectorId"), Required(ErrorMessage = "Selecione um setor!")]
    public int? SectorId { get; set; }
    public Sector Sector { get; set; }

    public bool ValidPassword(string pass)
    {
        return Cryptography.VerifyPasswordEncrypted(pass, Password);
    }

}
