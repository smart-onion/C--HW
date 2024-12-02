using hw7.Models;

namespace hw7.Services
{
    public class BookService
    {
        public List<Book> Books { get; set; }
        public BookService() { Books = new(); }
        public void Add(Book book) => Books.Add(book);
    }
}
