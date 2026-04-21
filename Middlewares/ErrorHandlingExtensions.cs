namespace AquaMonitor.Api.Middlewares
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
