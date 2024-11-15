using Lesson2.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<NumberInterpretationMiddleware>();
app.Run();

