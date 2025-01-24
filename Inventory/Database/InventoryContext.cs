using Inventory.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Database;

public class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
    }
    
    public DbSet<Item> Items { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}