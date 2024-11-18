using hw4.Services;
using hw4.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = "AdditionalTask";

if (builder.Environment.EnvironmentName == "MainTask")
{
    builder.Services.AddDataService();
}
else if (builder.Environment.EnvironmentName == "AdditionalTask")
{
    builder.Services.AddEFCoreService();
}

var app = builder.Build();


app.UseErrorHandler();
app.UseMyRouting();

if (builder.Environment.EnvironmentName == "AdditionalTask")
{
    app.UseStoreRouting();
}

app.Run();
