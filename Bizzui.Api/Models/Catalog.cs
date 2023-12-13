using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizzuiApi.Models;

public class Catalog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Price {get;set;}
    public required bool Active {get;set;} = true;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}
