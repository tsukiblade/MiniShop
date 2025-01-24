using Order.Abstractions;

namespace Order.Services.ServiceClients;

public class InventoryApiClient : IInventoryApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventoryApiClient> _logger;

    public InventoryApiClient(HttpClient httpClient, ILogger<InventoryApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> CheckAvailabilityAsync(Guid productId, int quantity)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/inventory/{productId}");
            if (!response.IsSuccessStatusCode)
                return false;

            var item = await response.Content.ReadFromJsonAsync<InventoryItemDto>();
            return item?.AvailableQuantity >= quantity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking product availability");
            return false;
        }
    }

    public async Task ReserveStockAsync(Guid productId, int quantity, string orderReference)
    {
        var request = new ReserveStockDto(quantity, orderReference);
        var response = await _httpClient.PostAsJsonAsync($"api/inventory/{productId}/reserve", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task ConfirmShipmentAsync(Guid productId, int quantity, string shipmentReference)
    {
        var request = new ShipStockDto(quantity, shipmentReference);
        var response = await _httpClient.PostAsJsonAsync($"api/inventory/{productId}/ship", request);
        response.EnsureSuccessStatusCode();
    }
}