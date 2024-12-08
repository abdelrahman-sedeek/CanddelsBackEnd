using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using System.Text.Json;

namespace CanddelsBackEnd.Infastrcuture
{
    public class StoreContextSeed
    {
        public static async Task SeedAync(CandelContext context)
        {
            if (!context.Categories.Any())
            {
                var CategoryDataData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/Category.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(CategoryDataData);

                // Add Category and save changes
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                var productData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                // Add Products and save changes
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
            if (!context.ProductVariants.Any())
            {
                var ProductVariantData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/product_variants.json");
                var productVariants = JsonSerializer.Deserialize<List<ProductVariant>>(ProductVariantData);

                // Add ProductTypes and save changes
                context.ProductVariants.AddRange(productVariants);
                await context.SaveChangesAsync();
            }

        

        }
    }
}
