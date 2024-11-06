var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var persons = new List<Person>()
{
    new Person { Id = 1, FirstName = "Alex", LastName = "LAlex", Age = 18, Email = "alex@mail.local"},
    new Person { Id = 2, FirstName = "Tom", LastName = "LTom", Age = 19, Email = "tom@mail.local"},
    new Person { Id = 3, FirstName = "Bob", LastName = "LBob", Age = 20, Email = "bob@mail.local"},
    new Person { Id = 4, FirstName = "Jon", LastName = "LLon", Age = 21, Email = "lon@mail.local"},
    new Person { Id = 5, FirstName = "Lini", LastName = "LLini", Age = 22, Email = "lini@mail.local"}
};

app.UseStaticFiles();

app.Run(async (context) =>
{
    var response = context.Response;
    await response.WriteAsJsonAsync(persons);
});

//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    if (context.Request.Path == "/")
//    {
//        await context.Response.SendFileAsync("wwwroot/index.html");
//    }
//    else if (context.Request.Path == "/uploadimage" && context.Request.Method == "POST")
//    {
//        IFormFile file = context.Request.Form.Files.GetFile("file");
//        if (file != null && file.Length > 0)
//        {
//            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);
//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream);
//            }
//            await context.Response.WriteAsync($"File {file.FileName} uploaded successfully!");
//        }
//        else
//        {
//            context.Response.StatusCode = 400;
//            await context.Response.WriteAsync("No file uploaded.");
//        }
//    }
//});

app.UseDeveloperExceptionPage();


app.Run();
class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}