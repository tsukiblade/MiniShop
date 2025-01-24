using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Entities;

namespace Order.Database.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Entities.Order>
{
    public void Configure(EntityTypeBuilder<Entities.Order> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.CustomerEmail).IsRequired().HasMaxLength(200);
        
        builder.Property(e => e.TotalAmount).HasPrecision(18, 2);
        
        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.ProductName).IsRequired().HasMaxLength(200);
        
        builder.Property(e => e.Price).HasPrecision(18, 2);
    }
}