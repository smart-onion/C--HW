using hw5.Model;
using hw5.Services;

namespace hw5.Middleware
{
    public class TokenAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenContext tokenContext;

        public TokenAuthorizationMiddleware(RequestDelegate next, TokenContext tokenContext)
        {
            _next = next;
            this.tokenContext = tokenContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var path = request.Path;
            var response = context.Response;

            var token = request.Cookies.FirstOrDefault(c => c.Key == "token");

            if (!tokenContext.IsRegisteredToken(token.Value))
            {
                request.Path = "/user/login";
            }
            
            await _next.Invoke(context);
        }
    }
}