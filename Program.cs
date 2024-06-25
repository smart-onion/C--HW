
using System.Collections;
using System.Reflection;

public class Program
 {
    public class Game
    {
        private int action;
        public int Action { get => this.action;}

        delegate void GameMenu();

        public Game()
        {
            this.action = -1;
        }

        public void Init()
        {
            GameMenu menu = Exit;
            menu += PlayGame;
            menu += LoadGame;
            menu += Settings;
            menu += About;



            while (this.action != 0)
            {
                Console.WriteLine("1 - Play Game");
                Console.WriteLine("2 - Load Game");
                Console.WriteLine("3 - Settings");
                Console.WriteLine("4 - About");
                Console.WriteLine("0 - Exit");

                Console.Write("Select number: ");
                this.action = Convert.ToInt32(Console.ReadLine());

               MethodInfo go = menu.GetInvocationList()[this.action].Method;
                go.Invoke(menu.GetInvocationList()[this.action].Target, null);
                Console.ReadKey();
                Console.Clear();
            }

        }

        public  void PlayGame()
        {
            Console.WriteLine("playing game...");
        }

        public void LoadGame()
        {
            Console.WriteLine("loading game...");
        }

        public void Settings()
        {
            Console.WriteLine("Settings");
        }

        public void About()
        {
            Console.WriteLine("About author..");
        }
        public void Exit()
        {
            Console.WriteLine("Exit game...");
        }
    }
    
    public static void Main()
    {
        Game game = new Game();
        game.Init();
    }
 }
