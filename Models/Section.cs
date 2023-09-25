using System.ComponentModel.DataAnnotations.Schema;

namespace SEP_Web.Models;
[Table("Sections")]
public class Section
{
    /* TO DO */
    public int Id { get; set; }
    public string Name { get; set; }
    public int DivisionId { get; set; }
    public Division Division { get; set; }
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }
}
