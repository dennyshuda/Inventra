using Inventra.Entities;

namespace Inventra.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Products.Any())
        {
            return;
        }

        var products = new Product[]
        {
            new() {
                Id = Guid.NewGuid(),
                Name = "Beras Premium 5kg",
                Sku = "BRS-001",
                PurchasePrice = 60000,
                SellingPrice = 65000,
                Stock = 50
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Minyak Goreng 2L",
                Sku = "MNG-002",
                PurchasePrice = 28000,
                SellingPrice = 32000,
                Stock = 20
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Gula Pasir 1kg",
                Sku = "GLA-003",
                PurchasePrice = 14000,
                SellingPrice = 16000,
                Stock = 100
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}