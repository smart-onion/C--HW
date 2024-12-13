using hw9.Models;
using hw9.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hw9.Components
{
    public class CommentsViewComponent: ViewComponent
    {
        private readonly BookContext bookContext;

        public CommentsViewComponent(BookContext bookContext) { this.bookContext = bookContext; }

        public async Task<IViewComponentResult> InvokeAsync(Book book)
        {
            var comments = await bookContext.Comments.Where(c => c.BookId == book.Id).ToListAsync();
            return View(comments);
        }
    }
}
