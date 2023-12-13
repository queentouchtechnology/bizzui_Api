using Microsoft.EntityFrameworkCore;
using BizzuiApi.Models;

namespace BizzuiApi.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // public AppDbContext(DbContextOptions<AppDbContext> options)
    // : base(options)
    // {}
    public DbSet<Catalog> Catalogs { get; set; } //= null!;
}