using hw5.Services;
using hw5.Model;

namespace hw5.Middleware
{
    public class MyServiceProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEnumerable<IMyServiceProvider> _services;
        private readonly TokenContext _tokenContext;
        public MyServiceProviderMiddleware(RequestDelegate next, IEnumerable<IMyServiceProvider> services, TokenContext tokenContext)
        {
            _next = next;
            _services = services;
            _tokenContext = tokenContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var path = request.Path;
            var response = context.Response;
            var dataProviders = context.RequestServices.GetService<IDataProvider>();
            response.ContentType = "text/html";
            if (path == "/user/login" && request.Method == HttpMethods.Post)
            {
                var user = new User
                {
                    Email = request.Form["email"],
                    Password = request.Form["password"]
                };


                var dbUser = await dataProviders.GetItem<User>(u => u.Email == user.Email && u.Password == user.Password);
                request.Method = HttpMethods.Get;
                if (dbUser == null)
                {
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    path = "/user/login";
                }
                else
                {
                    response.Cookies.Append("user", dbUser.Id.ToString());
                    var token = Guid.NewGuid().ToString();
                    _tokenContext.AddToken(token);
                    response.Cookies.Append("token", token);
                    path = "/home";
                }
            }
            if (path == "/user/login" && request.Method == HttpMethods.Get)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "login.html");
                await response.SendFileAsync(filePath);
            }

            
            if (path == "/home" && request.Method == HttpMethods.Get)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "home.html");
                await response.SendFileAsync(filePath);
            }

            if (path == "/services" && request.Method == HttpMethods.Get)
            {
                await response.WriteAsync(GetServicesHtml());
            }
            else if (path == "/services/get" && request.Method == HttpMethods.Get)
            {
                await response.WriteAsync(await GetRegisteredServicesHtml(context));

            }
            else
            {
                await _next.Invoke(context);
            }
        }

        string GetServicesHtml()
        {
            var html = $"""
                                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                    <title></title>
                </head>
                <body>
                    <nav>
                        <a href="/home">Home</a>
                    </nav>

                    <h3>Available Services</h3>

                """;
            
            foreach (var service in _services)
            {
                var name = service.GetType().Name;
                html += $"""
                <h4>{name}</h4>
                <form action="/{name}/register" method="post">
                    <input type="hidden" name="serviceName" value="{name}" />
                    <input type="text" name="description" value="" required />
                    <input type="submit" name="submit" value="Register" />
                </form>
                
                """;
            }
            html += "</body></html>";
            return html;
        }

        async Task<string> GetRegisteredServicesHtml(HttpContext context)
        {
            var userId = context.Request.Cookies.FirstOrDefault(x => x.Key == "user");
            var dataProvider = context.RequestServices.GetService<IDataProvider>();
            var user = await dataProvider.GetItem<User>(int.Parse(userId.Value));
            var services = await dataProvider.GetItems<Service>(user.Id);
            var uservices = await dataProvider.GetItems<UserService>(user.Id);
            var html = $"""
                                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                    <title></title>
                </head>
                <body>
                    <nav>
                        <a href="/home">Home</a>
                    </nav>

                    <h3>Available Services</h3>

                """;

            foreach (var service in services)
            {
                var name = service.Name;
                html += $"""
                <h4>{name}</h4>
                <p>{service.Description}</p>
                <form action="/{name}/edit/{service.Id}" method="post">
                    <input type="hidden" name="serviceName" value="{name}" />
                    <input type="text" name="description" value="{service.Description}" />
                    <input type="submit" name="submit" value="Edit" />
                </form>
                <form action="/{name}/remove/{service.Id}" method="post">
                    <input type="hidden" name="serviceName" value="{name}" />
                    <input type="submit" name="submit" value="Remove" />
                </form>
                """;
            }
            html += "</body></html>";
            return html;
        }
    }
}