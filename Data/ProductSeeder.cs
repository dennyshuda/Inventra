using Inventra.Models;

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

        var sembako = new Category { Name = "Sembako" };
        context.Categories.Add(sembako);
        context.SaveChanges();

        var products = new Product[]
        {
            new() {
            Id = Guid.NewGuid(),
            Name = "Beras Premium 5kg",
            Sku = "BRS-001",
            PurchasePrice = 60000,
            SellingPrice = 65000,
            Stock = 50,
            CategoryId = sembako.Id
            },
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}