using Microsoft.AspNetCore.Authentication.Cookies;
using MVCSTEP.Models;
using MVCSTEP.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<UserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization(opts =>
    opts.AddPolicy("Admin", policy => policy.Requirements.Add(new MinimumAgeRequirement(18)))
);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();