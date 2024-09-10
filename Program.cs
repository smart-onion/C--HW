using HW61;
using StateMachine;
internal class Program
{
    private static void Main(string[] args)
    {
        DbManager.SetDbContext(new ApplicationContext());
        StateManager<AppState> sm = new();
        sm.SetState(new MainMenu());
        sm.Run();

    }


}