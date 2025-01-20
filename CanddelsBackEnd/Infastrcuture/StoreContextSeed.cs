using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using System.Text.Json;

namespace CanddelsBackEnd.Infastrcuture
{
    public class StoreContextSeed
    {
        public static async Task SeedAync(CandelContext context)
        {
            // Seed Categories
            if (!context.Categories.Any())
            {
                var categoryData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/Category.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                // Add Categories and save changes
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Scents
            if (!context.Scents.Any())
            {
                var scentData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/scents.json");
                var scents = JsonSerializer.Deserialize<List<Scent>>(scentData);

                // Add Scents and save changes
                await context.Scents.AddRangeAsync(scents);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var productData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                // Add Products and save changes
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            // Seed ProductVariants
            if (!context.ProductVariants.Any())
            {
                var productVariantData = File.ReadAllText("../CanddelsBackEnd/Infastrcuture/Data/SeedData/product_variants.json");
                var productVariants = JsonSerializer.Deserialize<List<ProductVariant>>(productVariantData);

                // Add ProductVariants and save changes
                await context.ProductVariants.AddRangeAsync(productVariants);
                await context.SaveChangesAsync();
            }
        }
    }
}