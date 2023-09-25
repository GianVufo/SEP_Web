using System.ComponentModel.DataAnnotations.Schema;

namespace SEP_Web.Models;
[Table("Divisions")]
public class Division
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int InstituitionId { get; set; }
    public Instituition Instituition { get; set; }
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }
}
