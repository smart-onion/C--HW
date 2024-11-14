using hw3.Middleware;
using hw3.Model;
using Microsoft.EntityFrameworkCore;
using hw3.Html;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
await CreateDatabase();

app.MapWhen(
    context => context.Request.Path == "/regester-token",
    appbuilder => {
         appbuilder.UseMiddleware<RegesterTokenMiddleware>();
});

app.MapWhen(
    context => context.Request.Path == "/allbooks",
    async appbuilder =>
    {
        appbuilder.Run(async context =>
        {
            context.Response.ContentType = "text/html";
            List<Book> books = new List<Book>();
            using (var db = new ApplicationContext())
            {
                books = await db.Books.ToListAsync();
            }
            await context.Response.WriteAsync(HtmlBuilder.AllBooksPage(books));
        });
        

    });

app.UseMiddleware<TokenMiddleware>();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/getbook")
    {
        StringValues title;
        context.Request.Query.TryGetValue("title", out title);
        Book? book;
        using (var db = new ApplicationContext())
        {
            book = db.Books.FirstOrDefault(b => b.Title == title.ToString());
        }
        if (book == null) await context.Response.WriteAsync("not found");
        else await context.Response.WriteAsync(HtmlBuilder.BuildHtmlTemplate(book.Title, HtmlBuilder.ShowBook(book)));

    }
    else await next.Invoke();
});

app.Run();


static async Task CreateDatabase()
{
    using (ApplicationContext db = new ApplicationContext())
    {
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        db.Books.AddRange(new List<Book>()
        {
            new Book() {Title="Sherlok Holms", Author = "Arthur Conan Doyle",  Text = "here your Sherlok Holms book"},
            new Book() {Title="Romeo and Juliet", Author = "Shakespeare",  Text = "here your Romeo and Juliet book"},
            new Book() {Title="Test", Author = "Shakespeare",  Text = "here your Romeo and Juliet book"}
        });
        await db.SaveChangesAsync();
    }
}