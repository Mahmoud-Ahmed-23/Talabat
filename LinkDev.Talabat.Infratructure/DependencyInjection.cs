using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infratructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
			{
				var connectionstring = configuration.GetConnectionString("Redis");

				var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionstring!);

				return connectionMultiplexerObj;

			});

			return services;
		}
	}
}
