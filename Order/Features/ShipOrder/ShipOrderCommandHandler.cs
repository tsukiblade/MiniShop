using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Abstractions;
using Order.Database;

namespace Order.Features.ShipOrder;

public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, Result>
{
    private readonly OrderContext _context;
    private readonly IInventoryApiClient _inventoryClient;
    private readonly IEventPublisher _eventPublisher;

    public ShipOrderCommandHandler(
        OrderContext context, 
        IInventoryApiClient inventoryClient,
        IEventPublisher eventPublisher)
    {
        _context = context;
        _inventoryClient = inventoryClient;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result> Handle(ShipOrderCommand command, CancellationToken ct)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == command.OrderId, ct);
            
        if (order == null)
            return Result.Failure("Order not found");

        try
        {
            // Confirm shipment in inventory for all items
            foreach (var item in order.Items)
            {
                await _inventoryClient.ConfirmShipmentAsync(
                    item.ProductId,
                    item.Quantity,
                    $"SHIP-{order.Id}");
            }

            order.MarkAsShipped();
            await _context.SaveChangesAsync(ct);
            await _eventPublisher.PublishOrderShippedEvent(order);
            
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}