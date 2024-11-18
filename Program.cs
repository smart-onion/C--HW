using hw4.Services;
using hw4.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = "AdditionalTask";

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

if (builder.Environment.EnvironmentName == "MainTask")
{
    builder.Services.AddDataService();
}
else if (builder.Environment.EnvironmentName == "AdditionalTask")
{
    builder.Services.AddEFCoreService();
}

var app = builder.Build();


app.UseErrorHandler();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMyRouting();

if (builder.Environment.EnvironmentName == "AdditionalTask")
{
    app.UseStoreRouting();
}

app.Run();
