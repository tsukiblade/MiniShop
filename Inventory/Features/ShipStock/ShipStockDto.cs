namespace Inventory.Features.ShipStock;

public record ShipStockDto(
    int Quantity,
    string ShipmentReference,
    string? TrackingNumber,
    string? Carrier,
    DateTime ShipmentDate
);