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

    if (context.Request.Path == "/query")
    {
        string? name = request.Query["name"];
        string? email = request.Query["email"];
        string? phone = request.Query["phone"];

        await context.Response.WriteAsync($"<div><p>Name: {name}</p><br/>" +
            $"<p>Email: {email}</p><br/>" +
            $"<p>Phone: {phone}</p></div>"
            );
    }
    else
    {
        await context.Response.WriteAsync("User path with query: /query?name=Alex&email=email@mail.local&phone=+321654987");
    }
});
app.UseDeveloperExceptionPage();


app.Run();

