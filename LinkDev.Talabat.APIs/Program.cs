
using LinkDev.Talabat.APIs.Controllers;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infratructure.Persistence;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LinkDev.Talabat.Core.Application;
using Microsoft.AspNetCore.Mvc;
using LinkDev.Talabat.APIs.Middelwares;
using LinkDev.Talabat.Infratructure;
using Microsoft.AspNetCore.Identity;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LinkDev.Talabat.Infratructure.Persistence._Identity;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.APIs.Controllers.Errors;
namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);


			#region Services

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers()
				.ConfigureApiBehaviorOptions(options =>
				{
					options.SuppressModelStateInvalidFilter = false;
					options.InvalidModelStateResponseFactory = (actionContext) =>
					{
						var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
									   .Select(P => new ApiValidationErrorResponse.ValidationError()
									   {
										   Field = P.Key,
										   Errors = P.Value!.Errors.Select(e => e.ErrorMessage)
									   });
						return new BadRequestObjectResult(new ApiValidationErrorResponse()
						{
							Errors = errors
						});
					};

				})
				.AddApplicationPart(typeof(APIs.Controllers.AssemblyInformation).Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

			webApplicationBuilder.Services.AddApplicationServices();
			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddIdentityServices();

			//DependencyInjection.AddPersistenceServices(webApplicationBuilder.Services, webApplicationBuilder.Configuration);


			#endregion

			var app = webApplicationBuilder.Build();

			await app.InitializerStoreIdentityContextAsync();

			using var Scope = app.Services.CreateAsyncScope();

			var Services = Scope.ServiceProvider;

			var dbContext = Services.GetRequiredService<StoreDbContext>();

			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

			try
			{

				var pendingMigration = dbContext.Database.GetPendingMigrations();

				if (!pendingMigration.Any())
					await dbContext.Database.MigrateAsync();

				await StoreDbContextSeed.SeedAsync(dbContext);

			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an Error in Migration");
			}

			#region Configure Kestrel Middlewares

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();


			app.UseStatusCodePagesWithReExecute("/Errors/{0}");


			app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers();

			#endregion



			app.Run();
		}
	}
}
