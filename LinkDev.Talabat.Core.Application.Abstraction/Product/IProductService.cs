using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Product
{
	public interface IProductService
	{
		Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams);

		Task<ProductToReturnDto> GetProductAsync(int id);

		Task<IEnumerable<BrandDto>> GetBrandsAsync();

		Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

	}
}
