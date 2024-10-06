using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace LinkDev.Talabat.Infratructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<StoreContext>((options) =>
			{
				options.UseSqlServer(configuration.GetConnectionString("StoreContext"));
			});
			return services;
		}
	}
}
