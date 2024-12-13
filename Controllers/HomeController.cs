using hw9.Models;
using hw9.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace hw9.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookContext bookContext;
        public HomeController(BookContext bookContext) { this.bookContext = bookContext; }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetBook(int id)
        {
            var book = await bookContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> GetBook(Comment comment)
        {
            bookContext.Comments.Add(comment);
            await bookContext.SaveChangesAsync();
            return RedirectToAction(nameof(GetBook), new {id = comment.BookId});
        }

    }
}
