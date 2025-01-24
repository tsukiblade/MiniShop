using Inventory.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Database.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Reference)
            .HasMaxLength(100);
            
        builder.Property(x => x.CreatedBy)
            .HasMaxLength(50)
            .IsRequired();
            
        builder.Property(x => x.CreatedAt)
            .IsRequired();
            
        builder.HasIndex(x => new { x.ItemId, x.CreatedAt });
    }
}