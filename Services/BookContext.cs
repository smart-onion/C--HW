using hw9.Models;
using Microsoft.EntityFrameworkCore;

namespace hw9.Services
{
    public class BookContext: DbContext
    {
        static bool isCreated = false;
        public DbSet<Book>  Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public BookContext(DbContextOptions<BookContext> options) : base(options) 
        {
            if (!isCreated)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
                var localBooks = new List<Book>()
            {
                new Book() { Author = "Author1", Description = "Some description", Title = "Title1", PicturePath = "/images/book.svg" },
                new Book() { Author = "Author2", Description = "Some description", Title = "Title2", PicturePath = "/images/book.svg" },
                new Book() { Author = "Author3", Description = "Some description", Title = "Title3", PicturePath = "/images/book.svg" },
                new Book() { Author = "Author4", Description = "Some description", Title = "Title4", PicturePath = "/images/book.svg" },
                new Book() { Author = "Author5", Description = "Some description", Title = "Title5", PicturePath = "/images/book.svg" },
                new Book() { Author = "Author6", Description = "Some description", Title = "Title6", PicturePath = "/images/book.svg" }
            };
                Books.AddRange(localBooks);
                SaveChanges();
                isCreated = true;
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(c =>
            {
                c.HasOne(c => c.Book).WithMany(b => b.Comments).HasForeignKey(c => c.BookId);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
