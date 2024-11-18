
using hw4.Model;
using hw4.Services;
using System.Xml.Linq;

namespace hw4.Middleware
{
    public class RoutingMiddleware : Middleware
    {
        public RoutingMiddleware(RequestDelegate next) : base(next) { }
        public override async Task InvokeAsync(HttpContext context)
        {
            var dataService = context.RequestServices.GetService<IDataService>();
            var request = context.Request;
            var response = context.Response;    
            var path = request.Path;

            if (path == "/api/add-user")
            {
                var name = request.Query["name"].ToString();
                var ageParsed = int.TryParse(request.Query["age"], out int age);

                if (string.IsNullOrEmpty(name) || !ageParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                User user = new User() { Name = name, Age = age };
                var isAdded = await dataService.Add(user);
                if (!isAdded)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsync($"User {user.Name} added successfully");
            }
            else if (path == "/api/edit-user")
            {
                var name = request.Query["name"].ToString();
                var ageParsed = int.TryParse(request.Query["age"], out int age);
                var idParsed = int.TryParse(request.Query["id"], out int id);

                if (string.IsNullOrEmpty(name) || !ageParsed || !idParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                User user = new User() { Name = name, Age = age, Id = id };
                var isEdit = await dataService.Edit(user);
                if (!isEdit)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsync($"User {user.Name} edit successfully");
            }
            else if (path == "/api/remove-user")
            {
                var idParsed = int.TryParse(request.Query["id"], out int id);
                if (!idParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                var isRemoved = await dataService.Remove<User>(id);
                if (!isRemoved)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsync($"User {id} removed successfully");
            }
            else if (path == "/api/get-user")
            {
                var idParsed = int.TryParse(request.Query["id"], out int id);
                if (!idParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                var user = await dataService.Get<User>(id);
                await response.WriteAsJsonAsync(user);
            }
            else if (path == "/api/get-all")
            {
                var users = await dataService.GetAll<User>();
                await response.WriteAsJsonAsync(users);
            }
            else await _next.Invoke(context);
        }
    }
}
