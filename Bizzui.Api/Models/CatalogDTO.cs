using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizzuiApi.Models;

public class CatalogDTO
{
    public required string Name { get; set; } = "";
    public string? Description { get; set; } = "";
    public required string Platform { get; set; } = "";
    public required string Type { get; set; } = "";
    public required int Price {get;set;} = 0;
    public required int ValidityDays {get;set;} = 0;
    public required int DueDays {get;set;} = 0;
}
