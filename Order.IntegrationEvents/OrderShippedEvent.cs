namespace Order.IntegrationEvents;

public record OrderShippedEvent(
    Guid OrderId,
    string CustomerEmail,
    DateTime ShippedAt);