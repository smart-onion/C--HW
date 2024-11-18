namespace hw4.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
        public static IApplicationBuilder UseMyRouting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoutingMiddleware>();
        }

        public static IApplicationBuilder UseStoreRouting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StoreRoutingMiddleware>();
        }
    }
}
