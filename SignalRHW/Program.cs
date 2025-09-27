using SignalRHW.Hubs;
using SignalRHW.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<ActiveUsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseStaticFiles();
app.UseDefaultFiles();

app.MapHub<ActiveUserHub>("/active-users");
app.Run();
