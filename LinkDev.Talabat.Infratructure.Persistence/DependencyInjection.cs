﻿using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastruct.Persistence.Data;
using LinkDev.Talabat.Infratructure.Persistence._Identity;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using LinkDev.Talabat.Infratructure.Persistence.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace LinkDev.Talabat.Infratructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			#region Store DbContext

			services.AddDbContext<StoreDbContext>((options) =>
				{
					//options.UseSqlServer(configuration.GetConnectionString("StoreContext"));
					services.AddDbContext<StoreDbContext>((serviceProvider, options) =>
					{
						options
							.UseSqlServer(configuration.GetConnectionString("StoreContext"))
							.AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
					});
				});


			services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));

			services.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterceptor));

			#endregion


			#region Identity DbContext

			services.AddDbContext<StoreIdentityDbContext>((options) =>
			{
				options.UseSqlServer(configuration.GetConnectionString("IdentityContext"));
			});


			services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));


			#endregion


			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

			return services;
		}
	}
}
