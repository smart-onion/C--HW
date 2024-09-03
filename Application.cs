namespace HW3
{
    public class Application
    {
        public void Run()
        {
            while (true)
            {
                PageStack.pages.Peek().ShowPage();
            }
        }
    }
}
