using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(opts =>
{
    opts.UseInMemoryDatabase("Blog");
    /*opts.UseNpgsql(
        builder.Configuration["ConnectionStrings:DefaultConnection"]);*/
});

builder.Services.AddIdentity<User, IdentityRole>(opts =>
    {
        opts.Password.RequiredLength = 5; // минимальная длина
        opts.Password.RequireNonAlphanumeric = false; // требуются ли не алфавитно-цифровые символы
        opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
        opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
        opts.Password.RequireDigit = false; // требуются ли цифры
    })
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<IMembership, MembershipRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<IPublication, PublicationRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
        
        var applicationContext = services.GetRequiredService<ApplicationContext>();
        await ContentInitializer.InitializeAsync(applicationContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();