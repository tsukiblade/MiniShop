using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Database;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}