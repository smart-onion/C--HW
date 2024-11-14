using hw3.Model;
using Microsoft.EntityFrameworkCore;

namespace hw3.Middleware
{
    public class TokenMiddleware
    {
        private RequestDelegate next;
        public TokenMiddleware(RequestDelegate next) { this.next = next; }
        public async Task InvokeAsync(HttpContext context)
        {
            string? tokenString = context.Request.Query["token"];
            Token? token;
            if (tokenString == null)
            {
                await context.Response.WriteAsync("No token provided");
                return;
            }

            using (var db = new ApplicationContext())
            {
                token = db.Tokens.FirstOrDefault(t => t.MyToken == tokenString);
            }

            if (token == null)
            {
                await context.Response.WriteAsync($"Invalid token");
                return;
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
