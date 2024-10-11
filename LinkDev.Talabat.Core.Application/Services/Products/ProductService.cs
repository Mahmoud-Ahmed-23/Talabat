using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.ProductSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
	internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
	{

		public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
		{
			var specs = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex);

			var products = await unitOfWork.GetRepsitory<Product, int>().GetAllWithSpecAsync(specs);

			var MappedProducts = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

			return MappedProducts;
		}

		public async Task<ProductToReturnDto> GetProductAsync(int id)
		{
			var specs = new ProductWithBrandAndCategorySpecifications(id);

			var product = await unitOfWork.GetRepsitory<Product, int>().GetWithSpecAsync(specs);

			var MappedProduct = mapper.Map<ProductToReturnDto>(product);

			return MappedProduct;
		}

		public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
		=> mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepsitory<ProductBrand, int>().GetAllAsync());


		public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
		=> mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepsitory<ProductCategory, int>().GetAllAsync());

	}
}
