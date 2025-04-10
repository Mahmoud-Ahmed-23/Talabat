using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middelwares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infratructure;
using LinkDev.Talabat.Infratructure.Persistence;
using LinkDev.Talabat.Infratructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
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

			webApplicationBuilder.Services.AddHttpContextAccessor();
			webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

			webApplicationBuilder.Services.AddApplicationServices();
			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddIdentityServices();

			//DependencyInjection.AddPersistenceServices(webApplicationBuilder.Services, webApplicationBuilder.Configuration);


			#endregion

			var app = webApplicationBuilder.Build();

			await app.InitializerStoreIdentityContextAsync();

			

			#region Configure Kestrel Middlewares

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			await app.InitializerStoreIdentityContextAsync();

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
