using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
	{

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(string? sort, int? brandId, int? categoryId)
		{
			var products = await serviceManager.ProductService.GetProductsAsync(sort, brandId, categoryId);
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var Product = await serviceManager.ProductService.GetProductAsync(id);

			if (Product == null)
				return NotFound();

			return Ok(Product);
		}


		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
		{
			var Brands = await serviceManager.ProductService.GetBrandsAsync();
			return Ok(Brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
		{
			var categories = await serviceManager.ProductService.GetCategoriesAsync();
			return Ok(categories);
		}


	}
}
