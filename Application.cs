namespace HW42
{
    public static class Application
    {
        public static void Run()
        {
            DbManager.SetDbContext(new ApplicationContext());
            Navigation.MoveTo(new TabControl());
            while (true)
            {
                Navigation.GetStack().Peek().Display();
            }
        }
    }
}
