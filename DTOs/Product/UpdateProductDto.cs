namespace Inventra.DTOs.Product;

public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal? SellingPrice { get; set; }
    public int? Stock { get; set; }
    public int? CategoryId { get; set; }
}
