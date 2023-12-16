using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizzuiApi.Models;

[Table("Invoices", Schema ="dbo")]
public class Invoice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long CatalogId { get; set; }
    [DataType(DataType.Date)]
    public DateOnly StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateOnly EndDate { get; set; }
    [DataType(DataType.Date)]
    public DateOnly DueDate { get; set; }
    public required string Domain { get; set; }
    public required string Status { get; set; }
    public string? PaymentType { get; set; }
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}