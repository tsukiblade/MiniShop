using Inventory.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Database.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Sku)
            .HasMaxLength(50)
            .IsRequired();
            
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
            
        builder.Property(x => x.Price)
            .HasPrecision(18, 2);
            
        builder.Property(x => x.AvailableQuantity)
            .IsConcurrencyToken();
            
        builder.HasMany(x => x.Transactions)
            .WithOne(x => x.Item)
            .HasForeignKey(x => x.ItemId);
            
        builder.HasIndex(x => x.Sku)
            .IsUnique();
    }
}