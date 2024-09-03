namespace HW3
{
    public static class PageStack
    {
        public static Stack<Page> pages = new Stack<Page>();

        static PageStack()
        {
            pages.Push(new Login());
        }


    }
}
