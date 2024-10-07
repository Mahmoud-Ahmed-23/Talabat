using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using LinkDev.Talabat.Core.Application.Products;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application
{
	internal class ServiceManager : IServiceManager
	{

		private readonly Lazy<IProductService> _productService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
		}

		public IProductService ProductService => _productService.Value;



	}
}
