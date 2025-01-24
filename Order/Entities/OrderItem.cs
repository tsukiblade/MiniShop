namespace Order.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    
    private OrderItem() { } // For EF Core

    public static OrderItem Create(Guid productId, string productName, int quantity, decimal price)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero");

        return new OrderItem
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductName = productName,
            Quantity = quantity,
            Price = price
        };
    }
}