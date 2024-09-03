namespace HW3
{
    public class HomePage : Page
    {
        public override void NextPage(Page page)
        {
            throw new NotImplementedException();
        }

        public override void ShowPage()
        {
            Console.Clear();
            Console.WriteLine("Home page");
            Console.ReadKey();
        }
    }
}
