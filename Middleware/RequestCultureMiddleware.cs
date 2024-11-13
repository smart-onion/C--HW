namespace Lesson2.Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate next;

        public RequestCultureMiddleware(RequestDelegate next) { this.next = next; }

        public async Task InvodeAsync(HttpContext context)
        {

        }
    }
}
