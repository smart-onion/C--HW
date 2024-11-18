
using hw4.Model;
using hw4.Services;

namespace hw4.Middleware
{
    public class StoreRoutingMiddleware : Middleware
    {
        public StoreRoutingMiddleware(RequestDelegate next): base(next) { }
        public override async Task InvokeAsync(HttpContext context)
        {
            var dataService = context.RequestServices.GetService<IDataService>();

            var request = context.Request;
            var response = context.Response;
            var path = request.Path;

            if (path == "/api/store/get-product")
            {
                var idParsed = int.TryParse(request.Query["age"], out int id);

                if (!idParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                var product = await dataService.Get<Product>(id);
                if (product == null)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsJsonAsync(product);
            }
            else if (path == "/api/product/edit-user")
            {
                var name = request.Query["name"].ToString();
                var description = request.Query["description"].ToString();
                var priceParsed = double.TryParse(request.Query["price"], out double price);
                var idParsed = int.TryParse(request.Query["id"], out int id);

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || !idParsed || !priceParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                Product product = new Product() { Name = name, Description = description, Id = id , Price = price};
                var isEdit = await dataService.Edit(product);
                if (!isEdit)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsync($"Product {product.Name} edit successfully");
            }
            else if (path == "/api/product/remove-product")
            {
                var idParsed = int.TryParse(request.Query["id"], out int id);
                if (!idParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                var isRemoved = await dataService.Remove<Product>(id);
                if (!isRemoved)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsync($"Product {id} removed successfully");
            }
            else if (path == "/api/product/add-product")
            {
                var name = request.Query["name"].ToString();
                var description = request.Query["description"].ToString();
                var priceParsed = double.TryParse(request.Query["price"], out double price);

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || !priceParsed)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                Product product = new Product() { Name = name, Description = description , Price = price };
                var isAdded = await dataService.Add(product);
                if (!isAdded)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    return;
                }
                await response.WriteAsync($"Product {product.Name} added successfully");
            }
            else if (path == "/api/product/get-all")
            {
                var users = await dataService.GetAll<Product>();
                await response.WriteAsJsonAsync(users);
            }
            else await _next.Invoke(context);

        }
    }
}
