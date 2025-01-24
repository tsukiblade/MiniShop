using Order.Abstractions;

namespace Order.Services.ServiceClients;

// TODO: Fixup that
public static class InventoryApiClientExtensions
{
    public static IServiceCollection AddInventoryApiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IInventoryApiClient, InventoryApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["InventoryApi:BaseUrl"]!);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }
}