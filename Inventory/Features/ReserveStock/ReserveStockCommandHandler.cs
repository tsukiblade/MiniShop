using Inventory.Abstractions;
using Inventory.Database;
using Inventory.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Features.ReserveStock;

public class ReserveStockCommandHandler : IRequestHandler<ReserveStockCommand, Result>
{
    private readonly InventoryContext _context;
    private readonly ILogger<ReserveStockCommandHandler> _logger;

    public ReserveStockCommandHandler(InventoryContext context, ILogger<ReserveStockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(ReserveStockCommand command, CancellationToken ct)
    {
        var item = await _context.Items.FindAsync([command.ItemId], ct);
        if (item == null)
            return Result.Failure("Item not found");

        if (item.AvailableQuantity < command.Quantity)
            return Result.Failure("Insufficient stock");

        var transaction = new Transaction
        {
            ItemId = item.Id,
            Type = TransactionType.Reservation,
            Quantity = command.Quantity,
            Reference = command.OrderReference,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system"
        };

        item.AvailableQuantity -= command.Quantity;
        item.ReservedQuantity += command.Quantity;
        item.UpdatedAt = DateTime.UtcNow;

        _context.Transactions.Add(transaction);

        try
        {
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure("Concurrent modification detected");
        }
    }
}