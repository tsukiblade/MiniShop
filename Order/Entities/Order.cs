namespace Order.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string CustomerEmail { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    private Order() { } // For EF Core

    public static Order Create(string customerEmail, List<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new ArgumentException("Customer email is required");
        if (!items.Any())
            throw new ArgumentException("Order must contain at least one item");

        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerEmail = customerEmail,
            Status = OrderStatus.Created,
            Items = items,
            TotalAmount = items.Sum(i => i.Price * i.Quantity),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Order must be in Created state to be marked as paid");

        Status = OrderStatus.Paid;
        PaidAt = DateTime.UtcNow;
    }

    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be in Paid state to be marked as shipped");

        Status = OrderStatus.Shipped;
        ShippedAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Order must be in Shipped state to be marked as delivered");

        Status = OrderStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;
    }
}