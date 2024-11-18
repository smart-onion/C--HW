namespace hw4.Middleware
{
    public abstract class Middleware
    {
        protected RequestDelegate _next;
        protected Middleware(RequestDelegate next)
        {
            _next = next;
        }
        public abstract Task InvokeAsync(HttpContext context);
    }
}
