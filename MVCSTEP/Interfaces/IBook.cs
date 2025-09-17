using MVCSTEP.Models;

namespace MVCSTEP.Interfaces;

public interface IBook
{
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookAsync(int id);
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
}