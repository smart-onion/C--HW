using hw3.Model;

namespace hw3.Middleware
{
    public class RegesterTokenMiddleware
    {
        private RequestDelegate next;
        public RegesterTokenMiddleware(RequestDelegate next) { this.next = next; }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = Guid.NewGuid().ToString()[..5];
            using (var db = new ApplicationContext())
            {
                await db.Tokens.AddAsync(new Token { MyToken = token });
                await db.SaveChangesAsync();
                await context.Response.WriteAsync($"Your token: {token}");
            }
            await next.Invoke(context);
        }
    }
}
