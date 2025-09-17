using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Application.Handlers;
using MVCSTEP.Core.Interfaces;
using MVCSTEP.Infrastructure.Database_Context;
using MVCSTEP.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MVCSTEP.Application.AccessHandlers;
using MVCSTEP.Application.AccessHandlers.Requirements;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Infrastructure.Profiles;
using MVCSTEP.WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IReview, ReviewRepository>();
builder.Services.AddScoped<IJwtService, JwtServiceRepository>();
builder.Services.AddScoped<IAccountService, IAccountServiceRepository>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetProductByIdCommandHandler).Assembly);
});

builder.Services.AddOpenApi();
builder.Services.AddControllers().AddDataAnnotationsLocalization();

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = JwtSettings.ISSUER,
        ValidateAudience = true,
        ValidAudience = JwtSettings.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = JwtSettings.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("ProductOwner", policy => policy.Requirements.Add(new IsProductOwnerRequirement()));
    opts.AddPolicy("ReviewOwner", policy => policy.AddRequirements(new IsReviewOwnerRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, IsProductOwnerHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsReviewOwnerHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(cfg => { }, typeof(ProductProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    await InitUsers.InitializeAsync(userManager, roleManager);
}

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();