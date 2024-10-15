
using LinkDev.Talabat.APIs.Controllers;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infratructure.Persistence;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;
using LinkDev.Talabat.APIs.Middelwares;

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
									   .SelectMany(p => p.Value!.Errors)
									   .Select(E => E.ErrorMessage);
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
			//DependencyInjection.AddPersistenceServices(webApplicationBuilder.Services, webApplicationBuilder.Configuration);



			#endregion

			var app = webApplicationBuilder.Build();

			using var Scope = app.Services.CreateAsyncScope();

			var Services = Scope.ServiceProvider;

			var dbContext = Services.GetRequiredService<StoreContext>();

			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

			try
			{
				var pendingMigration = dbContext.Database.GetPendingMigrations();

				if (!pendingMigration.Any())
					await dbContext.Database.MigrateAsync();

				await StoreContextSeed.SeedAsync(dbContext);
			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an Error in Migration");
			}

			#region Configure Kestrel Middlewares

			app.UseMiddleware<CustomExceptionHandlerMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers();

			#endregion



			app.Run();
		}
	}
}
