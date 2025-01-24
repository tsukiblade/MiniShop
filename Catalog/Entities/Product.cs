namespace Catalog.Entities;

public class Product
{
     public Guid Id { get; set; }

     public Guid InventoryId { get; set; }
     
     public string Name { get; set; } = string.Empty;
     public string Description { get; set; } = string.Empty;
     public decimal Price { get; set; }
     public bool IsActive { get; set; }

     public Category Category { get; set; } = null!;

     public IList<string> Tags { get; set; } = new List<string>();
}