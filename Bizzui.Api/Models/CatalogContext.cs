using Microsoft.EntityFrameworkCore;

namespace BizzuiApi.Models;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options)
    {
    }

    public DbSet<Catalog> Catalogs { get; set; } = null!;
}