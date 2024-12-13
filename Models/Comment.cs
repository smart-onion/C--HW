namespace hw9.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
