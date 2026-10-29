
namespace Lucas_The_Tavern_Order_Management_System.Models;

public class Product
{
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
