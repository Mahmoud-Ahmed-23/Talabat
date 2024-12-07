using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence._Common;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastruct.Persistence.Data
{
	internal class StoreDbInitializer(StoreDbContext _dbContext) : DbInitializer(_dbContext), IStoreDbInitializer
	{

		public override async Task SeedAsync()
		{
			if (!_dbContext.Brands.Any())
			{
				var brandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infratructure.Persistence/Data/Seeds/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				if (Brands?.Count() > 0)
				{
					await _dbContext.Brands.AddRangeAsync(Brands);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.Categories.Any())
			{
				var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infratructure.Persistence/Data/Seeds/categories.json");
				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

				if (Categories?.Count() > 0)
				{
					await _dbContext.Categories.AddRangeAsync(Categories);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.Products.Any())
			{
				var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infratructure.Persistence/Data/Seeds/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (Products?.Count() > 0)
				{
					await _dbContext.Products.AddRangeAsync(Products);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.DeliveryMethods.Any())
			{
				var deliveryMethodsData = await File.ReadAllTextAsync(@"..\LinkDev.Talabat.Infratructure.Persistence\_Data\Seeds\delivery.json");
				var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
				if (deliveryMethods?.Count > 0)
				{
					await _dbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
