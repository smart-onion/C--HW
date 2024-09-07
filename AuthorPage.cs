namespace HW42
{
    internal class AuthorPage : Page
    {
        public AuthorPage() : base(PageList.Authors) { }
        public override void Display()
        {
            Console.WriteLine("===== Author Page =====");
            Console.WriteLine("1 - Add Author");
            Console.WriteLine("2 - Edit Author");
            Console.WriteLine("3 - Remove Author");
            Console.WriteLine("4 - Tab Control");
            Console.Write("Select action: ");
            int action = Utility.GetInt();
            switch (action)
            {
                case 1:
                    DbManager.AddItem(AddAuthor());
                    break;
                case 2:
                    EditAuthor();
                    break;
                case 3:
                    RemoveAuthor();
                    break;
                case 4:
                    Navigation.MoveTo(new TabControl());
                    break;
                default:
                    break;
            }
        }

        Author AddAuthor()
        {
            Console.Write("Enter Author name: ");
            var name = Console.ReadLine();
            return new Author { Name = name };
        }

        void RemoveAuthor()
        {
            ShowAuthors();
            Console.WriteLine("Select author by id: ");
            int authId = Utility.GetInt();
            DbManager.GetDbContext().Authors.Remove(new Author { Id = authId });
        }

        void EditAuthor()
        {
            ShowAuthors();
            Console.WriteLine("Select author to edit: ");
            int authId = Utility.GetInt();
            Console.Write("Enter new Name: ");
            var newName = Console.ReadLine();
            var auth = DbManager.GetDbContext().Authors.FirstOrDefault(a => a.Id == authId);
            auth.Name = newName;
            DbManager.GetDbContext().SaveChanges();
        }

        void ShowAuthors()
        {
            Console.WriteLine("Authors: ");
            var auths = DbManager.GetDbContext().Authors.ToList();
            Utility.ShowTable(auths);

        }
    }
}
