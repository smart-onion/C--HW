using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
    if (!db.Tasks.Any())
    {
        db.Tasks.AddRange(
            new TaskItem { Title = "Sample task 1" },
            new TaskItem { Title = "Sample task 2", Description = "Details..." }
        );
        db.SaveChanges();
    }
}

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();
