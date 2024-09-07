namespace HW42
{
    public class TabControl : Page
    {
        public TabControl() : base(PageList.TabControl) { }
        public override void Display()
        {
            Console.Clear();
            Console.WriteLine("+----- Tab Control -----+");
            Page.ShowPages();
            Console.WriteLine("+-----------------------|");
            Console.Write("Select tab: ");
            PageList input = (PageList)Utility.GetInt();
            switch (input)
            {
                case PageList.Books:
                    Navigation.MoveTo(new BookPage());
                    break;
                case PageList.Authors:
                    Navigation.MoveTo(new AuthorPage());
                    break;
                case PageList.Publishers:
                    Navigation.MoveTo(new PublisherPage());
                    break;
                case PageList.TabControl:
                    break;
                default:
                    break;
            }
        }
    }
}
