using Microsoft.EntityFrameworkCore;

namespace hw3.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ASP3;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }

}
