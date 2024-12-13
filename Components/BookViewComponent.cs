using hw9.Services;
using hw9.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hw9.Components
{
    public class BookViewComponent : ViewComponent
    {
        private readonly BookContext bookContext;

        public BookViewComponent(BookContext bookContext) { this.bookContext = bookContext; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = bookContext.Books;
            var bvm = await books

                .Select(b => new BookViewModel { Id = b.Id, Author = b.Author, Title = b.Title, PicturePath = b.PicturePath })
                .ToListAsync();
            return View(bvm);
        }
}
}
