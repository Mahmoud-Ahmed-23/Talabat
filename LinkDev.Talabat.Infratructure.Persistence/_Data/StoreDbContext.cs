using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.Data
{
	public class StoreDbContext : DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }
	}
}
