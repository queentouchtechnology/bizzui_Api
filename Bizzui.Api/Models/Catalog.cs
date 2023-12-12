namespace BizzuiApi.Models;

public class Catalog
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Platform {get;set;}
    public required string Type {get;set;}
    public required int Price {get;set;}
    public required int ValidityDays {get;set;}
    public required int DueDays {get;set;} = 10;
    public required bool Active {get;set;} = true;
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;


}
