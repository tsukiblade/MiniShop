using MediatR;
using Order.Abstractions;
using Order.Database;
using Order.Entities;

namespace Order.Features.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly OrderContext _context;
    private readonly IInventoryApiClient _inventoryClient;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly IUserContext _userContext;

    public CreateOrderCommandHandler(
        OrderContext context,
        IInventoryApiClient inventoryClient,
        IEventPublisher eventPublisher,
        ILogger<CreateOrderCommandHandler> logger,
        IUserContext userContext)
    {
        _context = context;
        _inventoryClient = inventoryClient;
        _eventPublisher = eventPublisher;
        _logger = logger;
        _userContext = userContext;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        try
        {
            // Check stock availability for all items
            foreach (var item in command.Items)
            {
                var isAvailable = await _inventoryClient.CheckAvailabilityAsync(
                    item.ProductId, item.Quantity);
                
                if (!isAvailable)
                    return Result<Guid>.Failure($"Product {item.ProductId} is not available in requested quantity");
            }

            // Create order items
            var orderItems = command.Items.Select(item =>
                    OrderItem.Create(item.ProductId, item.ProductName, item.Quantity, item.Price))
                .ToList();

            // Create order
            var order = Entities.Order.Create(_userContext.Email, orderItems);
            _context.Orders.Add(order);

            // Reserve stock for all items
            foreach (var item in order.Items)
            {
                await _inventoryClient.ReserveStockAsync(
                    item.ProductId, 
                    item.Quantity,
                    order.Id.ToString());
            }

            await _context.SaveChangesAsync(ct);
            await _eventPublisher.PublishOrderCreatedEvent(order);

            return Result<Guid>.Success(order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return Result<Guid>.Failure("Error creating order");
        }
    }
}