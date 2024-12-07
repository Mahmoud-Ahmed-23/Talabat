using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Data
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            if (!dbContext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("E:\\Course Dot Net (.Net)\\Apis\\APIs Project\\LinkDev.Talabat\\LinkDev.Talabat.Infratructure.Persistence\\Data\\Seeds\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await dbContext.Brands.AddRangeAsync(brands);

                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("E:\\Course Dot Net (.Net)\\Apis\\APIs Project\\LinkDev.Talabat\\LinkDev.Talabat.Infratructure.Persistence\\Data\\Seeds\\categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    await dbContext.Categories.AddRangeAsync(Categories);

                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("E:\\Course Dot Net (.Net)\\Apis\\APIs Project\\LinkDev.Talabat\\LinkDev.Talabat.Infratructure.Persistence\\Data\\Seeds\\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    await dbContext.Products.AddRangeAsync(Products);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
