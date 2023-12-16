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

public class CreateInvoiceRqstMdl
{
    [Required(ErrorMessage = "Customer is required")]
    public required long CustomerId {get;set;}
    [Required(ErrorMessage = "Catalog is required")]
    public required long CatalogId { get; set; }

    [Required(ErrorMessage = "Domain is required")]
    public required string Domain { get; set; }
}
public class UpdateInvoicePaymentRqstMdl
{
    [Required(ErrorMessage = "Status is required")]
    public required string Status {get;set;}
    [Required(ErrorMessage = "Payment Type is required")]
    public required string PaymentType { get; set; }
}