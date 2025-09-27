using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;

var adminRole = new Role("admin");
var userRole = new Role("user");
var people = new List<Person>
{
    new Person("admin@gmail.com", "qwerty", adminRole),
    new Person("alex@gmail.com", "192837", userRole),
};
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();
 
builder.Services.AddSignalR();
 
var app = builder.Build();
 
app.UseAuthentication();   // добавление middleware аутентификации 
app.UseAuthorization();   // добавление middleware авторизации 
 
app.MapGet("/login", async context =>
    await SendHtmlAsync(context, "wwwroot/login.html"));
 
app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
{
    // получаем из формы email и пароль
    var form = context.Request.Form;
    // если email и/или пароль не установлены, посылаем статусный код ошибки 400
    if (!form.ContainsKey("email") || !form.ContainsKey("password"))
    {
        return Results.BadRequest("Email and/or password are not set");
    }
    string email = form["email"];
    string password = form["password"];
 
    // находим пользователя 
    Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
    // если пользователь не найден, отправляем статусный код 401
    if (person is null)
    {
        return Results.Unauthorized();
    }
 
    var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
    };
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    await context.SignInAsync(claimsPrincipal);
 
    return Results.Redirect(returnUrl ?? "/");
});
 
app.MapGet("/", [Authorize] async (HttpContext context) =>
    await SendHtmlAsync(context, "wwwroot/index.html"));
 
app.MapGet("/admin", [Authorize(Roles = "admin")] async (HttpContext context) =>
    await SendHtmlAsync(context, "wwwroot/admin.html"));
 
app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});
 
app.MapHub<ChatHub>("/chat");
app.Run();
 
async Task SendHtmlAsync(HttpContext context, string path)
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync(path);
}
record class Person(string Email, string Password, Role Role);
record class Role(string Name);