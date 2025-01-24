namespace Order.Abstractions;

public interface IInventoryApiClient
{
    Task<bool> CheckAvailabilityAsync(Guid productId, int quantity);
    Task ReserveStockAsync(Guid productId, int quantity, string orderReference);
    Task ConfirmShipmentAsync(Guid productId, int quantity, string shipmentReference);
}