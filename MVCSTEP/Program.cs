using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.AuthHandlers;
using MVCSTEP.AuthHandlers.Requirements;
using MVCSTEP.Data;
using MVCSTEP.Filters;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Repositories;
using MVCSTEP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(opts => { opts.Filters.Add<ModelValidateFilter>(); });

builder.Services.AddScoped<IAuthorizationHandler, IsNoteOwnerHandler>();
builder.Services.AddScoped<IAuthorizationHandler, PublicationAccessHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsPublicationOwnerHandler>();
builder.Services.AddScoped<NoteOwnerAuthFilter>();
builder.Services.AddScoped<IEmailSender, ConsoleEmailSenderService>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(opts =>
    {
        opts.User.RequireUniqueEmail = true;
        opts.SignIn.RequireConfirmedEmail = true;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequiredLength = 4;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("NoteOwner", policy => policy.AddRequirements(new IsNoteOwnerRequirement()));
    opts.AddPolicy("PublicationAccess", policy => policy.AddRequirements(new PublicationAccessRequirement()));
    opts.AddPolicy("PublicationOwner", policy => policy.AddRequirements(new IsPublicationOwnerRequirement()));
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IFriend, FriendRepository>();
builder.Services.AddScoped<IPublication, PublicationRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await UsersInitialize.InitializeAsync(userManager, roleManager);
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
app.UseStaticFiles();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();