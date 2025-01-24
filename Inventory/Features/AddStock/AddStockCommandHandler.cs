using Inventory.Abstractions;
using Inventory.Database;
using Inventory.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Features.AddStock;

public class AddStockCommandHandler : IRequestHandler<AddStockCommand, Result>
{
    private readonly InventoryContext _context;
    private readonly ILogger<AddStockCommandHandler> _logger;

    public AddStockCommandHandler(InventoryContext context, ILogger<AddStockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(AddStockCommand command, CancellationToken ct)
    {
        var item = await _context.Items.FindAsync([command.ItemId], ct);
        if (item == null)
            return Result.Failure("Item not found");

        var transaction = new Transaction
        {
            ItemId = item.Id,
            Type = TransactionType.Addition,
            Quantity = command.Quantity,
            Reference = command.Reference,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system" // TODO: Get from auth context
        };

        item.AvailableQuantity += command.Quantity;
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