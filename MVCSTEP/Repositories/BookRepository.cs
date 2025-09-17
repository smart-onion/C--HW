using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class BookRepository: IBook
{
    private readonly ApplicationContext _context;

    public BookRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<Book>> GetBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public Task<Book> GetBookAsync(int id)
    {
        return _context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        var record = new UpdateBookRecord()
        {
            BookId = book.Id,

        };
        
        _context.UpdateBookRecords.Add(record);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}