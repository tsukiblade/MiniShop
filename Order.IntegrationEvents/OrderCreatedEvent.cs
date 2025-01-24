namespace Order.IntegrationEvents;

public record OrderCreatedEvent(
    Guid OrderId,
    string CustomerEmail,
    decimal TotalAmount,
    DateTime CreatedAt);