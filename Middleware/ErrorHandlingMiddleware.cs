namespace Lesson2.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 400)
            {
                await context.Response.WriteAsync("Number exceed range from 1 to 100000");
            }
            else if (context.Response.StatusCode == 402)
            {
                await context.Response.WriteAsync("bad data");

            }
            else if (context.Response.StatusCode == 402)
            {
                await context.Response.WriteAsync("Not found");

            }
        }
    }
}
