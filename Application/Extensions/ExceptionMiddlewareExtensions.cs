using System.Net;
using BLL.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;

namespace Application.Extensions
{
	public static class ExceptionMiddlewareExtensions
	{
		public static void ConfigureExceptionHandler(this WebApplication app, Serilog.ILogger logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						if (contextFeature.Error is ValidationException validationException)
						{
							context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
							context.Response.ContentType = "application/json";

							List<ErrorModel> errors =
								validationException.Errors.Select(x =>
									new ErrorModel(x.PropertyName, x.ErrorMessage)).ToList();

							logger.Warning("Invalid input model: {@errors}", errors);
							await context.Response.WriteAsJsonAsync(errors);
						}
						else
						{
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
							context.Response.ContentType = "application/json";
							logger.Error("Something went wrong: {@error}", contextFeature.Error);
							var message = "InternalServerError";
							if (!app.Environment.IsProduction())
							{
								message = contextFeature.Error.Message;
							}

							await context.Response.WriteAsJsonAsync(
								new InternalErrorModel(message));
						}
					}
				});
			});
		}
	}
}
