using Microsoft.AspNetCore.Mvc;
using hw7.Models;
using hw7.ViewModels;
using hw7.Services;
namespace hw7.Controllers
{
    public class BookController : Controller
    {
        BookService books;

        public BookController(BookService books)
        {
            this.books = books;
        }

        public IActionResult Index()
        {
            var bvm = books.Books.Select( b => new BookViewModel() { Title = b.Title, Author = b.Author, Genre = b.Genre, Year = b.Year});
            return View(bvm);
        }
        
        public IActionResult Add()
        {
            return View(nameof(Add));
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {
                books.Add(book);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Content("Not valid data");
            }
        }
    }
}
