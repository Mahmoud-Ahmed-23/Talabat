using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction
{
	public interface IServiceManager
	{
		IProductService ProductService { get; }
		IBasketService BasketService { get; }
		IAuthService AuthService { get; }

	}
}
