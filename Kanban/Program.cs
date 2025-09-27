using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Repositories;
using Kanban.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Максимальное количество повторных попыток
            maxRetryDelay: TimeSpan.FromSeconds(30), // Максимальное время ожидания между попытками
            errorCodesToAdd: null // null или пустой список - использует стандартные ошибки для повтора
        );
    });
});

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Configuration"]));
builder.Services.AddSingleton<RedisCacheService>();

builder.Services.AddScoped<IBoard, BoardRepository>();
builder.Services.AddScoped<ICard, CardRepository>();
builder.Services.AddScoped<IList, ListRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.MapOpenApi();
// }

app.MapControllers();
app.Run();