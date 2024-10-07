using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepsitory<ProductBrand, int>().GetAllAsync());

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepsitory<ProductCategory, int>().GetAllAsync());

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        => mapper.Map<ProductToReturnDto>(await unitOfWork.GetRepsitory<Product, int>().GetAsync(id));

        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        => mapper.Map<IEnumerable<ProductToReturnDto>>(await unitOfWork.GetRepsitory<Product, int>().GetAllAsync());
    }
}
