using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<ApplicationContext>(opts => opts.UseInMemoryDatabase("db"));
builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();