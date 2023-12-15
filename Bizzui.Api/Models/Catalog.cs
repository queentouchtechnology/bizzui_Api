using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizzuiApi.Models;

[Table("Catalogs", Schema ="dbo")]
public class Catalog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Platform { get; set; }
    public required string Type { get; set; }
    public required int Price {get;set;}
    public required int ValidityDays {get;set;}
    public required int DueDays {get;set;}
    public required bool Active {get;set;} = true;
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}

public class CatalogDto
{
    public required string Name { get; set; } = "";
    public string? Description { get; set; } = "";
    public required string Platform { get; set; } = "";
    public required string Type { get; set; } = "";
    public required int Price {get;set;} = 0;
    public required int ValidityDays {get;set;} = 0;
    public required int DueDays {get;set;} = 0;
}
