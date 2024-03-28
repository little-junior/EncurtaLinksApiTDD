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
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new
                    {
                        context.Response.StatusCode,
                        Error = "Internal Server Error",
                        Message = "Ocorreu um erro no servidor"
                    });
                });
            });
        }
    }
}
