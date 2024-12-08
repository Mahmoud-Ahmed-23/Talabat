using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Talabat.Infratructure.Persistence.UnitOfWork;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Services.Basket;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Application.Abstraction.Order;
using LinkDev.Talabat.Core.Application.Services.Orders;
namespace LinkDev.Talabat.Core.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

			services.AddAutoMapper(typeof(MappingProfile));

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


			services.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
			{
				//var mapper = serviceProvider.GetRequiredService<IMapper>();
				//var configuration = serviceProvider.GetRequiredService<IConfiguration>();
				//var repository = serviceProvider.GetRequiredService<IBasketRepository>();

				//return () => new BasketService(repository, mapper, configuration);
				return () => serviceProvider.GetServices<IOrderService>();
			});

			services.AddScoped(typeof(IOrderService), typeof(OrderService));

			services.AddScoped(typeof(IBasketService), typeof(BasketService));
			//services.AddScoped(typeof(Func<IBasketService>), typeof(Func<BasketService>));

			services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
			{
				//var mapper = serviceProvider.GetRequiredService<IMapper>();
				//var configuration = serviceProvider.GetRequiredService<IConfiguration>();
				//var repository = serviceProvider.GetRequiredService<IBasketRepository>();

				//return () => new BasketService(repository, mapper, configuration);
				return () => serviceProvider.GetServices<IBasketService>();
			});

			return services;
		}
	}
}
