namespace HW3
{
    public class RegisterPage : Page
    {
        User user = new User();
        public override void NextPage(Page page)
        {
            PageStack.pages.Push(page);
        }

        public override void ShowPage()
        {
            Console.Clear();
            Console.WriteLine("===== Register page =====");
            Console.Write("Username: ");
            user.Username = Console.ReadLine();
            Console.Write("Password: ");
            user.Password = Console.ReadLine();
            if (UserService.Register(user))
            {
                Console.WriteLine($"User {user.Username} registered successfully!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                NextPage(new Login());
            }

        }
    }
}
