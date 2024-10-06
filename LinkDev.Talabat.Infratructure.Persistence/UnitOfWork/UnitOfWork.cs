using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using LinkDev.Talabat.Infratructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure.Persistence.UnitOfWork
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;

		private readonly Lazy<IGenericRepsitory<Product, int>> _productRepostory;
		private readonly Lazy<IGenericRepsitory<ProductBrand, int>> _brandRepostory;
		private readonly Lazy<IGenericRepsitory<ProductCategory, int>> _categoryRepostory;

		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_productRepostory = new Lazy<IGenericRepsitory<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
			_brandRepostory = new Lazy<IGenericRepsitory<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
			_categoryRepostory = new Lazy<IGenericRepsitory<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));
		}
		public IGenericRepsitory<Product, int> ProductRepository => _productRepostory.Value;
		public IGenericRepsitory<ProductBrand, int> BrandRepository => _brandRepostory.Value;
		public IGenericRepsitory<ProductCategory, int> CategoryRepository => _categoryRepostory.Value;

		public Task<int> CompleteAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
