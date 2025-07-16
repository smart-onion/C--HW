using hw1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var users = new List<User>();

app.UseStaticFiles();



app.MapGet("/",async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var path = context.Request.Path;

	switch (path)
    
	{
		case "/":
            await context.Response.SendFileAsync("wwwroot/index.html");
            break;
		case "/fillup":
            await context.Response.SendFileAsync("wwwroot/fillup.html");
            break;
		case "/api/postuser":
            if (context.Request.Method == "POST")
            {
                var form = context.Request.Form;
                string name = form["name"];
                string email = form["email"];
                string phone = form["phone"];
                users.Add(new User() { Name = name, Email = email, PhoneNumber = phone });
                context.Response.Redirect("/greeting");
            }
            break;
        case "/api/greeting":
            if (context.Request.Method == "POST")
            {
                var form = context.Request.Form;
                string name = form["name"];
                await context.Response.WriteAsync($"<p>Hello {name}</p>");
            }
            else context.Response.Redirect("/greeting");
            break;
        case "/api/customers":
            await context.Response.WriteAsJsonAsync(users);
            break;
        case "/greeting":
            await context.Response.SendFileAsync("wwwroot/greet.html");
            break;
		default:
			break;
	}
});

app.MapGet("/api/length/{str}", async (string str, HttpContext context) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await context.Response.WriteAsJsonAsync(new { String=str, Length=str.Length });
});
app.UseDeveloperExceptionPage();

app.Run();
