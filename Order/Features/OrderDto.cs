namespace Order.Features;

public record OrderDto(
    Guid Id,
    string Status,
    decimal TotalAmount,
    DateTime CreatedAt,
    DateTime? PaidAt,
    List<OrderItemDto> Items);
