namespace HW42
{
    public abstract class Page
    {
        public readonly PageList pageList;

        public Page(PageList pageList)
        {
            this.pageList = pageList;
        }

        public abstract void Display();
        public virtual void MoveTo(Page page) 
        {
            Navigation.MoveTo(page);
        }

        public static void ShowPages()
        {
            int i = 1;
            foreach (var item in Enum.GetNames(typeof(PageList)))
            {
                if (item != "TabControl")
                {
                    Console.WriteLine("{0} - {1}", i++, item);
                }
            }
        }
    }


}
