namespace Order.Features.CreateOrder;

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal Price);