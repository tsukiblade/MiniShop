using MediatR;
using Order.Abstractions;
using Order.Database;

namespace Order.Features.DeliverOrder;

public class DeliverOrderCommandHandler : IRequestHandler<DeliverOrderCommand, Result>
{
    private readonly OrderContext _context;
    private readonly IEventPublisher _eventPublisher;

    public DeliverOrderCommandHandler(OrderContext context, IEventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result> Handle(DeliverOrderCommand command, CancellationToken ct)
    {
        var order = await _context.Orders.FindAsync([command.OrderId], ct);
        if (order == null)
            return Result.Failure("Order not found");

        try
        {
            order.MarkAsDelivered();
            await _context.SaveChangesAsync(ct);
            await _eventPublisher.PublishOrderDeliveredEvent(order);
            
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}