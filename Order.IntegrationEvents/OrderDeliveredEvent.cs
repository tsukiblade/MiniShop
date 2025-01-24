namespace Order.IntegrationEvents;

public record OrderDeliveredEvent(
    Guid OrderId,
    string CustomerEmail,
    DateTime DeliveredAt);
