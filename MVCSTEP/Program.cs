using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.AuthHandlers;
using MVCSTEP.AuthHandlers.Requirements;
using MVCSTEP.Data;
using MVCSTEP.Filters;
using MVCSTEP.Models;
using MVCSTEP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(opts => { opts.Filters.Add<ModelValidateFilter>(); });

builder.Services.AddScoped<IAuthorizationHandler, IsNoteOwnerHandler>();
builder.Services.AddScoped<NoteOwnerAuthFilter>();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseInMemoryDatabase("db"));

builder.Services.AddIdentity<User, IdentityRole>(opts => { opts.User.RequireUniqueEmail = true; })
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("NoteOwner", policy => policy.AddRequirements(new IsNoteOwnerRequirement()));
});

var app = builder.Build();

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