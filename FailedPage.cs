namespace HW3
{
    public class FailedPage : Page
    {
        public override void NextPage(Page page)
        {
            PageStack.pages.Push(page);
        }

        public override void ShowPage()
        {
            Console.Clear();
            Console.WriteLine("Authentication failed!");
            Console.Write("Press 1 - try again, 2 - register new user: ");
            var action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    PageStack.pages.Pop();
                    break;
                case 2:
                    NextPage(new RegisterPage());
                    break;
                default:
                    break;
            }

        }
    }
}
