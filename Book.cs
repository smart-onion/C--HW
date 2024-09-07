
namespace HW42
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int? PublisherId { get; set; }

        public Publisher? Publisher { get; set; }
        public Author Author { get; set; }

    }
}
