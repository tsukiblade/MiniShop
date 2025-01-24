namespace Inventory.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public TransactionType Type { get; set; }
    public int Quantity { get; set; }
    public string? Reference { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Item Item { get; set; } = null!;
}