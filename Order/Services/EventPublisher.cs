using MassTransit;
using Order.Abstractions;
using Order.IntegrationEvents;

namespace Order.Services;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(IBus publishEndpoint, ILogger<EventPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishOrderCreatedEvent(Entities.Order order)
    {
        var @event = new OrderCreatedEvent(
            order.Id,
            order.CustomerEmail,
            order.TotalAmount,
            order.CreatedAt);

        await PublishEvent(@event);
    }

    public async Task PublishOrderPaidEvent(Entities.Order order)
    {
        var @event = new OrderPaidEvent(
            order.Id,
            order.CustomerEmail,
            order.TotalAmount,
            order.PaidAt!.Value);

        await PublishEvent(@event);
    }

    public async Task PublishOrderShippedEvent(Entities.Order order)
    {
        var @event = new OrderShippedEvent(
            order.Id,
            order.CustomerEmail,
            order.ShippedAt!.Value);

        await PublishEvent(@event);
    }

    public async Task PublishOrderDeliveredEvent(Entities.Order order)
    {
        var @event = new OrderDeliveredEvent(
            order.Id,
            order.CustomerEmail,
            order.DeliveredAt!.Value);

        await PublishEvent(@event);
    }

    private async Task PublishEvent<T>(T @event) where T : class
    {
        try
        {
            await _publishEndpoint.Publish(@event);
            _logger.LogInformation("Published event {EventType} with data: {@Event}", 
                typeof(T).Name, @event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish event {EventType}", typeof(T).Name);
            throw;
        }
    }
}