using System.ComponentModel.DataAnnotations.Schema;

namespace SEP_Web.Models;
[Table("Sectors")]
public class Sector
{
    /* TO DO */
    public int Id { get; set; }
    public string Name { get; set; }
    public int SectionId { get; set; }
    public Section Section { get; set; }
    public int UserAdministratorId { get; set; }
    public UserAdministrator UserAdministrator { get; set; }
}
