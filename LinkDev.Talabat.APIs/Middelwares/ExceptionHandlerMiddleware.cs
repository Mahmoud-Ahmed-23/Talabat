using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.Middelwares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly IWebHostEnvironment _env;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				#region Logging : TODO

				if (_env.IsDevelopment())
				{
					_logger.LogError(ex, ex.Message);
				}
				else
				{

				}

				#endregion

				await HandelExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
		{
			ApiResponse response;

			switch (ex)
			{
				case NotFoundException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(404, ex.Message);
					await httpContext.Response.WriteAsync(response.ToString());

					break;

				case BadRequestException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(400, ex.Message);
					await httpContext.Response.WriteAsync(response.ToString());

					break;

				default:

					response = _env.IsDevelopment()
						? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
						: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

					httpContext.Response.ContentType = "application/json";

					await httpContext.Response.WriteAsync(response.ToString());

					break;
			}
		}
	}
}
