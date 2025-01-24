using MediatR;
using Order.Abstractions;
using Order.Database;

namespace Order.Features.PayOrder;

public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Result>
{
    private readonly OrderContext _context;
    private readonly IEventPublisher _eventPublisher;

    public PayOrderCommandHandler(OrderContext context, IEventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result> Handle(PayOrderCommand command, CancellationToken ct)
    {
        var order = await _context.Orders.FindAsync([command.OrderId], ct);
        if (order == null)
            return Result.Failure("Order not found");

        try
        {
            order.MarkAsPaid();
            await _context.SaveChangesAsync(ct);
            await _eventPublisher.PublishOrderPaidEvent(order);
            
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}