namespace HW42
{
    public static class Navigation
    {
        static Dictionary<PageList, Page> pageMap = new Dictionary<PageList, Page>();

        static Stack<Page> pageStack = new Stack<Page>();

        public static void MoveTo(Page page)
        {
            if (!pageMap.ContainsKey(page.pageList))
            {
                pageMap.Add(page.pageList, page);
            }

            pageStack.Push(pageMap[page.pageList]);
        }

        public static Stack<Page> GetStack()
        {
            return pageStack;
        }
    }
}
