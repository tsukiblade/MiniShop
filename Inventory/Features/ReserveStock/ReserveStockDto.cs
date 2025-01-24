namespace Inventory.Features.ReserveStock;

public record ReserveStockDto(
    int Quantity,
    string OrderReference,
    DateTime? ExpirationDate,
    string? CustomerReference
);