using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork : IAsyncDisposable
	{



		IGenericRepsitory<Product, int> ProductRepository { get; set; }
		IGenericRepsitory<ProductBrand, int> BrandRepository { get; set; }
		IGenericRepsitory<ProductCategory, int> CategoryRepository { get; set; }

		Task<int> CompleteAsync(); 

	}
}
