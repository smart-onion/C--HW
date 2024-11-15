using Humanizer;

namespace Lesson2.Middleware
{
    public class NumberInterpretationMiddleware
    {
        private readonly RequestDelegate _next;
        public NumberInterpretationMiddleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext context)
        {
            if (int.TryParse(context.Request.Query["number"], out int number))
            {
                if (number > 1 && number <= 100000)
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync($"Your number {number.ToWords()}");
                }
                else context.Response.StatusCode = 400;
            }
            else context.Response.StatusCode = 402;

            //await _next(context);

        }
    }
}
