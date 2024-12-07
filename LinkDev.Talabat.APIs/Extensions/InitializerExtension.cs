using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infratructure.Persistence._Identity;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class InitializerExtension
	{
		public static async Task<WebApplication> InitializerStoreIdentityContextAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;

			var storeContextIntializer = services.GetRequiredService<IStoreDbInitializer>();
			var storeIdentityContextIntializer = services.GetRequiredService<IStoreIdentityDbInitializer>();

			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				await storeContextIntializer.InitializeAsync();
				await storeContextIntializer.SeedAsync();

				await storeIdentityContextIntializer.InitializeAsync();
				await storeIdentityContextIntializer.SeedAsync();

			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "an error has been occured during applaying migrations");
			}

			return app;
		}
	}
}
