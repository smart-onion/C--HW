using StateMachine;
using System;

namespace HW61
{
    public class CompanyState : State<AppState>
    {
        public CompanyState() : base(AppState.COMPANIES) { }
        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("===== Company =====");
            Console.WriteLine("1 - Add Company");
            Console.WriteLine("2 - Edit Company");
            Console.WriteLine("3 - Remove Company");
            Console.WriteLine("4 - Back to main menu");
            Console.Write("Select action: ");
        }

        public override void Execute()
        {
            int action = Utility.GetInt();

            switch (action)
            {
                case 1:
                    DbManager.AddItem(AddCompany());
                    break;
                case 2:
                    EditCompany();
                    break;
                case 3:
                    DbManager.RemoveItem(RemoveCompany());
                    break;
                case 4:
                    OnChangeState += () => { return new MainMenu(); };
                    break;
                default:
                    break;
            }
        }

        Company AddCompany()
        {
            Console.Write("Enter Name: ");
            var name = Console.ReadLine();

            return new Company { Name = name };
        }

        void EditCompany()
        {
            ShowCompanies();
            Console.Write("Select company to edit by id: ");
            int companyId = Utility.GetInt();

            var company = DbManager.GetDbContext().Companies.FirstOrDefault(e => e.Id == companyId);

            var newCompany = AddCompany();

            company = newCompany;
        }

        Company? RemoveCompany()
        {
            ShowCompanies();
            int companyId = Utility.GetInt();
            return DbManager.GetDbContext().Companies.FirstOrDefault(e => e.Id == companyId);
        }

        public static void ShowCompanies()
        {
            Console.WriteLine("Companies:");
            var companies = DbManager.GetDbContext().Companies.ToList();
            Utility.ShowTable(companies);
        }
    }
}
