using System.Runtime.CompilerServices;

namespace HW42
{
    internal class PublisherPage : Page
    {
        public PublisherPage() : base(PageList.Publishers) { }
        public override void Display()
        {
            Console.Clear();
            Console.WriteLine("===== Publisher Page ======");
            Console.WriteLine("1 - Add publisher");
            Console.WriteLine("2 - Edit Publisher");
            Console.WriteLine("3 - Remove Publisher");
            Console.WriteLine("4 - Tab control");
            Console.Write("Select action: ");
            int action = Utility.GetInt();

            switch (action)
            {
                case 1:
                    DbManager.AddItem(AddPublisher());
                    break;
                case 2:
                    EditPublisher();
                    break;
                case 3:
                    DbManager.RemoveItem(RemovePublisher());
                    break;
                case 4:
                    Navigation.MoveTo(new TabControl());
                    break;
                default:
                    break;
            }
        }

        Publisher AddPublisher()
        {
            var publisher = new Publisher();
            Console.Write("Enter Name: ");
            var pubName = Console.ReadLine();
            Console.Write("Enter address: ");
            var pubAddr = Console.ReadLine();
            Console.Write("Enter email: ");
            var pubEmail = Console.ReadLine();

            publisher.Name = pubName;
            publisher.Address = pubAddr;
            publisher.Email = pubEmail;
            return publisher;
        }

        Publisher RemovePublisher()
        {
            

            Console.Write("Enter publisher id to remove: ");

            int id = Utility.GetInt();
            return new Publisher { Id = id };
        }

        void EditPublisher()
        {
            ShowPublihsers();
            Console.Write("Choose publisher to edit: ");
            int pubId = Utility.GetInt();
            var pubToEdit = DbManager.GetDbContext().Publishers.FirstOrDefault(p => p.Id == pubId);
            var newPub = AddPublisher();
            pubToEdit = newPub;
            DbManager.GetDbContext().SaveChanges();
        }

        void ShowPublihsers()
        {
            Console.WriteLine("publishers: ");
            var pubs = DbManager.GetDbContext().Publishers.ToList();
            Utility.ShowTable(pubs);
        }
    }
}
