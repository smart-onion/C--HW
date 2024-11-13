using Lesson2.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(CheckMethods);

app.MapWhen(
    context => context.Request.Method == HttpMethods.Get,
    appbuilder =>
    {
        appbuilder.Use(IfGetOnly);
    });


app.Use(IfPostAndJson);
app.Use(IsPersonObj);

app.Run(async context => await context.Response.WriteAsync("Hi"));
app.Run();

static async Task CheckMethods(HttpContext context, RequestDelegate next)
{
    if (context.Request.Method == HttpMethods.Get || context.Request.Method == HttpMethods.Post)
    {
        await next.Invoke(context);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"{context.Request.Method} restricted");
    }
}

static async Task IfGetOnly(HttpContext context, RequestDelegate next)
{
    if (context.Request.Method == HttpMethods.Get)
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Welcome to Person API!");
    }
}

static async Task IfPostAndJson(HttpContext context, RequestDelegate next)
{
    if (context.Request.ContentType == "application/json") await next.Invoke(context);
    else
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync($"{context.Request.ContentType} restricted");
    }
}

static async Task IsPersonObj(HttpContext context, RequestDelegate next)
{
    string jsonString;
    using (var reader = new StreamReader(context.Request.Body))
    {
        jsonString = await reader.ReadToEndAsync();
    }
    var person = JsonSerializer.Deserialize<Person>(jsonString);
    if (person == null)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync($"{jsonString} bad format");
        return;
    }
    Console.WriteLine($"Person name: {person.Name}, Age: {person.Age}");
    await next.Invoke(context);
}
record Person(string Name, int Age);

