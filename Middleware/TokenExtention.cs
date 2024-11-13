namespace Lesson2.Middleware
{
    public static class TokenExtention
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string token)
        {
            return builder.UseMiddleware<TokenMiddleware>(token);
        }
    }
}
