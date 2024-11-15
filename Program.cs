using Lesson2.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDirectoryBrowser();
    app.UseDeveloperExceptionPage();
    app.Run(async context => await context.Response.WriteAsync("You on Develepment stage"));

}
else if (app.Environment.IsStaging())
{
    app.UseDirectoryBrowser();
    app.Run(async context => await context.Response.WriteAsync("You on Staging stage.Some tools can be blocked"));
}

else if (!app.Environment.IsProduction())
{
    app.Run(async context =>
    {
        Console.WriteLine($"{DateTime.Now} path: {context.Request.Path}");
        if (context.Request.Path == "/wwwroot")
        {
            await context.Response.WriteAsync("Not allowed on production");
        }
    });
}

app.Run();


