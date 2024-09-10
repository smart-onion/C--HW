using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StateMachine;

namespace HW61
{
    public class UserState : State<AppState>
    {
        public UserState() : base(AppState.USERS) { }
        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("===== Users =====");
            Console.WriteLine("1 - Add user");
            Console.WriteLine("2 - Edit user");
            Console.WriteLine("3 - Remove user");
            Console.WriteLine("4 - Back to main menu");
            Console.Write("Select action: ");
        }


        public override void Execute()
        {
            int action = Utility.GetInt();

            switch (action)
            {
                case 1:
                    DbManager.AddItem(AddUser());
                    break;
                case 2:
                    EditUser();
                    break;
                case 3:
                    DbManager.RemoveItem(RemoveUser());
                    break;
                case 4:
                    OnChangeState += () => { return new MainMenu(); };
                    break;
                default:
                    break;
            }
        }
        User AddUser()
        {
            Console.Write("Enter Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Age: ");
            var age = Utility.GetInt();
            CompanyState.ShowCompanies();
            Console.Write("Enter Company Id: ");
            var companyId = Utility.GetInt();

            return new User { Name = name, Age = age, CompanyId = companyId };
        }

        void EditUser()
        {
            ShowUsers();
            Console.Write("Select user to edit by id: ");
            int userId = Utility.GetInt();

            var user = DbManager.GetDbContext().Users.FirstOrDefault(e => e.Id == userId);

            var newUser = AddUser();

            user = newUser;
        }

        User? RemoveUser()
        {
            ShowUsers();
            int userId = Utility.GetInt();
            return DbManager.GetDbContext().Users.FirstOrDefault(e => e.Id == userId);
        }

        public static void ShowUsers()
        {
            Console.WriteLine("Users:");
            var users = DbManager.GetDbContext().Users.ToList();
            Utility.ShowTable(users);
        }
    }

    
}
