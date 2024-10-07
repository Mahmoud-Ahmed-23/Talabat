using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

			services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}
	}
}
