namespace Order.Services.ServiceClients;

public record InventoryItemDto(
    Guid Id,
    string Sku,
    string Name,
    int AvailableQuantity,
    int ReservedQuantity,
    decimal Price);