using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizzuiApi.Models;

[Table("Users", Schema ="dbo")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Mobile { get; set; }
    public required string Role { get; set; }
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}

public class UserRqstMdl
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Mobile { get; set; }
    public required string Role { get; set; }
}

public class UserRspnMdl
{
    public long Id { get; set; }
    public required string Email { get; set; }
    public required string Mobile { get; set; }
    public required string Role { get; set; }
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
}
