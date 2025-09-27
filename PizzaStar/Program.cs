using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Data;
using PizzaStar.Interfaces;
using PizzaStar.Models;
using PizzaStar.Repositories;

// 1. Forgot password
// 2. create or edit role

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
IConfigurationRoot _confString = new ConfigurationBuilder().
    SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
 
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(_confString.GetConnectionString("DefaultConnection"))
     // options.UseInMemoryDatabase("db")
    );


builder.Services.AddIdentity<User, IdentityRole>(opts =>
    {
        opts.Password.RequiredLength = 4;   // минимальная длина
        opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
        opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
        opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
        opts.Password.RequireDigit = false; // требуются ли цифры
    })
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<ICategory, CategoryRepository>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var context = services.GetRequiredService<ApplicationContext>();
        await DbInit.InitializeAsync(userManager, rolesManager);
        await DbInit.InitializeContentAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();