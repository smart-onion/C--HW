using StateMachine;

namespace HW61
{
    public class MainMenu : State<AppState>
    {
        public MainMenu() : base(AppState.MENU) { }
        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("===== Main Menu =====");
            Console.WriteLine("1 - Users");
            Console.WriteLine("2 - Companies");
            Console.WriteLine("3 - Show all");
            Console.WriteLine("4 - Exit");
            Console.Write("Select action: ");
        }

        public override void Execute()
        {
            int action = Utility.GetInt();

            switch (action)
            {
                case 1:
                    OnChangeState += () => { return new UserState(); };
                    break;
                case 2:
                    OnChangeState += () => { return new CompanyState(); };
                    break;
                case 3:
                    UserState.ShowUsers();
                    CompanyState.ShowCompanies();
                    Console.Write("Enter any key to continue...");
                    Console.ReadKey();
                    break;
                case 4:
                    OnChangeState += () =>
                    {
                        return null;
                    };
                    break;
                default:
                    break;
            }
        }
    }
}
