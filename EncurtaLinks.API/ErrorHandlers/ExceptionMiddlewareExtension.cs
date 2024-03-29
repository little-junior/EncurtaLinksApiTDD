using EncurtaLinks.API.ErrorHandlers;
using EncurtaLinks.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace EncurtaLinks.API.ErrorHandler
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (error is not null)
                    {
                        ErrorResponse errorResponse;

                        if (error is CustomException customException)
                        {
                            context.Response.StatusCode = customException.StatusCode;
                            errorResponse = new ErrorResponse(customException.StatusCode, customException.Error, customException.Message);
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            errorResponse = new ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error", "Ocorreu um erro no servidor");
                        }

                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }
                });
            });
        }
    }
}
