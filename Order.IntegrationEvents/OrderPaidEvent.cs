namespace Order.IntegrationEvents;

public record OrderPaidEvent(
    Guid OrderId,
    string CustomerEmail,
    decimal TotalAmount,
    DateTime PaidAt);