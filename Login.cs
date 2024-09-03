namespace HW3
{
    public class Login : Page
    {
        private User user;

        public Login() 
        {
            user = new User();
        }

        public override void ShowPage()
        {
            Console.Clear();
            Console.WriteLine("====== Login Page ======");
            Console.Write("Username: ");
            user.Username = Console.ReadLine();
            Console.Write("Password: ");
            user.Password = Console.ReadLine();
            Authentication();
        }

        private void Authentication()
        {
            if (UserService.VerifyUser(this.user))
            {
                NextPage(new HomePage());
            }
            else
            {
                NextPage(new FailedPage());
            }
        }

        public override void NextPage(Page page)
        {
            PageStack.pages.Push(page);
        }
    }
}
