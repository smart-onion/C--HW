
namespace hw4.Middleware
{
    public class ErrorHandlerMiddleware : Middleware
    {
        public ErrorHandlerMiddleware(RequestDelegate next) : base(next) { }
        public override async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);
            var response = context.Response;
            switch (response.StatusCode)
            {
                case StatusCodes.Status400BadRequest:
                {
                    await response.WriteAsync("Bad request");
                    break;
                }
                case StatusCodes.Status422UnprocessableEntity:
                {
                        await response.WriteAsync("Unprocessable Entity");
                        break;
                }
                default:
                    break;
            }
        }
    }
}
