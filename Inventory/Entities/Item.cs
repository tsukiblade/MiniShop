namespace Inventory.Entities;

public class Item
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int AvailableQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
}