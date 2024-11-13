using Lesson2.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string date = "";


app.Use(LogRequest);
app.Use(LogHeaders);
app.Use(LogErrors);
app.Run(async context => await context.Response.WriteAsync("Hi"));
app.Run();

static async Task LogRequest(HttpContext context, RequestDelegate next)
{
    var request = context.Request;
    var log = new StringBuilder();
    log.Append($"{DateTime.Now.ToString()} method: {request.Method} path: {request.Path} endpoint: {request.Host}");
    Console.WriteLine(log);
    await next.Invoke(context);
}

static async Task LogHeaders(HttpContext context, RequestDelegate next)
{
    var response = context.Response;
    var log = new StringBuilder();

    foreach ( var header in response.Headers)
    {
        log.AppendLine($"{header.Key} - {header.Value}");
    }
    await next.Invoke(context);
}

static async Task LogErrors(HttpContext context, RequestDelegate next)
{
    var request = context.Request;
    var path = request.Path;
    var log = new StringBuilder();

    if (path == "/error-page")
    {
        log.AppendLine($"Restricted access: {path}");
    }
    else if (request.Query["error"] == "true")
    {
        log.AppendLine($"Restricted query: {request.Query["error"]}");
    }
    else await next.Invoke(context);

}