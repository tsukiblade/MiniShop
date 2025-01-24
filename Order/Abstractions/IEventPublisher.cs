namespace Order.Abstractions;

public interface IEventPublisher
{
    Task PublishOrderCreatedEvent(Entities.Order order);
    Task PublishOrderPaidEvent(Entities.Order order);
    Task PublishOrderShippedEvent(Entities.Order order);
    Task PublishOrderDeliveredEvent(Entities.Order order);
}