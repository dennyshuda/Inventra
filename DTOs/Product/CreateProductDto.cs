namespace Inventra.DTOs.Product;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}
