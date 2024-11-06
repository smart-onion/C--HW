using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();


app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var path = context.Request.Path.ToString();
    var response = context.Response;
    var request = context.Request;
    var part = Path.GetFileName(path);

    if (request.Path == $"/api/length/{part}")
    {
        await response.WriteAsync($"Length = {part.Length}");
    }

});
app.UseDeveloperExceptionPage();


app.Run();

