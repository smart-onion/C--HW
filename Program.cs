using Azure.Core;
using Azure;
using hw5.Middleware;
using hw5.Model;
using hw5.Services;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenContext>();
builder.Services.AddTransient<IDataProvider, ServiceContext>();
builder.Services.AddTransient<IMyServiceProvider, ShopServiceProvider>();
builder.Services.AddTransient<IMyServiceProvider, FlyTicketServiceProvider>();

var app = builder.Build();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    var request = context.Request;
    var path = request.Path;
    var response = context.Response;
    var dataProviders = context.RequestServices.GetService<IDataProvider>();

    if (path == "/user/register" && request.Method == HttpMethods.Post)
    {
        var user = new User
        {
            Name = request.Form["name"],
            Email = request.Form["email"],
            Password = request.Form["password"]
        };
        await dataProviders.AddItem<User>(user);
        //response.ContentType = "text/html";
        //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "login.html");
        //await response.SendFileAsync(filePath);
        request.Path = "/user/login";
        request.Method = HttpMethods.Get;
    }

    else if (path == "/user/register" && request.Method == HttpMethods.Get)
    {
        response.ContentType = "text/html";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "register.html");
        await response.SendFileAsync(filePath);
    }
    await next(context);
});

app.UseMiddleware<TokenAuthorizationMiddleware>();
app.UseMiddleware<MyServiceProviderMiddleware>();

app.Map("/{serviceName}/{action}/{id?}", async (string serviceName, string action, int? id, HttpContext context) =>
{
    var services = context.RequestServices.GetServices<IMyServiceProvider>();
    var dataProvider = context.RequestServices.GetService<IDataProvider>();
    IMyServiceProvider? myService = null;
    string name = null;
    foreach (var service in services)
    {
        name = service.GetType().Name;
        if (name == serviceName)
        {
            myService = service;
        }
    }

    var userId = context.Request.Cookies.FirstOrDefault(x => x.Key == "user");
    if (string.IsNullOrEmpty(userId.Value) || userId.Value == null)
    {
        await context.Response.WriteAsync("Bad Request");
    }

    var user = await dataProvider.GetItem<User>(int.Parse(userId.Value));

    if (user == null) await context.Response.WriteAsync("Bad Request");

    if (myService == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Not found");
    }
    else if (action == "register")
    {
        var description = context.Request.Form["description"].ToString();
        await myService.Register(user, new Service() { Name = serviceName, Description = description });
        context.Response.Redirect("/services/get");

    }
    else if (action == "edit")
    {
        var userService = await dataProvider.GetItem<Service>(s => s.Id == id);
        if (userService != null)
        {
            var description = context.Request.Form["description"].ToString();
            userService.Description = description;
            await myService.Edit(user, userService);
            context.Response.Redirect("/services/get");
        }

    }
    else if (action == "remove")
    {
        var userService = await dataProvider.GetItem<Service>(s => s.Id == id);
        await dataProvider.RemoveItem<UserService>(new UserService { ServiceId = userService.Id, UserId = user.Id });
        context.Response.Redirect("/services/get");
    }
    else if (action == "get")
    {
        await context.Response.WriteAsync(await myService.GetServiceInfo(user.Id));
    }

});

app.Run();
