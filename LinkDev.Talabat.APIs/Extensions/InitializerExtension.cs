using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infratructure.Persistence.Data;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class InitializerExtension
	{
		public static async Task<WebApplication> InitializerStoreIdentityContextAsync(this WebApplication app)
		{
			using var Scope = app.Services.CreateAsyncScope();

			var Services = Scope.ServiceProvider;

			var dbContext = Services.GetRequiredService<IStoreIdentityDbInitializer>();

			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

			try
			{
				await dbContext.InitializeAsync();
				await dbContext.SeedAsync();
			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an Error in Migration");
			}

			return app;
		}
	}
}
