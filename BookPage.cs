namespace HW42
{
    public class BookPage : Page
    {
        public BookPage() : base(PageList.Books) { }


        public override void Display()
        {
            Console.Clear();
            Console.WriteLine("===== Book page =====");
            Console.WriteLine("1 - Add book");
            Console.WriteLine("2 - Edit book");
            Console.WriteLine("3 - Remove book");
            Console.WriteLine("4 - Tab control");
            Console.Write("Select action: ");
            
            int action = Utility.GetInt();
            

            switch (action)
            {
                case 1:
                    DbManager.AddItem(AddBook());
                    break;
                case 2:
                    EditBook();

                    break;
                case 3:
                    DbManager.RemoveItem(RemoveBook());
                    break;
                case 4:
                    Navigation.MoveTo(new TabControl());
                    break;
                default:
                    break;
            }
        }

        Book AddBook()
        {
            var book = new Book();

            Console.Clear();
            Console.Write("Enter book name: ");
            var bookName = Console.ReadLine();

            var authors = DbManager.GetDbContext().Authors.ToList();
            var publichers = DbManager.GetDbContext().Publishers.ToList();
            Utility.ShowTable(authors);
            Console.WriteLine("Choose Author (enter id): ");
            var author = Utility.GetInt();
            Utility.ShowTable(publichers);
            Console.Write("Choose Publisher (enter id): ");
            var pablisher = Utility.GetInt();

            book.Name = bookName;
            book.AuthorId = author;
            book.PublisherId = pablisher;

            return book;
        }

        Book RemoveBook()
        {
            ShowBooks();
            Console.Write("Choose book to remove (enter id): ");
            int book = Utility.GetInt();
            return new Book { Id = book };
        }

        void EditBook()
        {
            ShowBooks();
            Console.Write("Choose book to edit (by id): ");
            int bookId = Utility.GetInt();
            var book = DbManager.GetDbContext().Books.FirstOrDefault(b => b.Id == bookId);
            var newBook = AddBook();
            book = newBook;
            DbManager.GetDbContext().SaveChanges();
        }

        void ShowBooks()
        {
            var books = DbManager.GetDbContext().Books.ToList();
            Console.WriteLine("Books in db: ");
            Utility.ShowTable(books);
        }
    }
}
