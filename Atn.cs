namespace Anthill
{
    public abstract class Ant
    {
        protected readonly int timeForStep = 300;
        protected int x, y;
        protected static Map map;
        public char Body { get; protected set; }
        protected int Age { get; set; }
        protected Random rand = new Random();
        protected event Action OnStep;    
        protected Ant(char body, int age)
        {
            Body = body;
            Age = age;
            Spawn();
        }

        public static void AsignMap(Map map)
        {
            Ant.map = map; 
        }

        protected virtual void Spawn()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            do
            {
                x = rand.Next(1, Map.width - 1);
                y = rand.Next(1, Map.height - 1);
            }
            while (map.labyrinth[y,x] == '#');
            Console.SetCursorPosition(x, y);
            Console.Write(Body);
        }

        protected void NextStep()
        {
            int currX = x;
            int currY = y;

            while (true)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
                x = currX;
                y = currY;
                int direction = rand.Next(4);
                switch (direction)
                {
                    case 0: // Up
                        if (y > 0 && map.labyrinth[y - 1,x] != '#') y--;
                        break;
                    case 1: // Right
                        if (x < Map.width - 1 && map.labyrinth[y, x + 1] != '#') x++;
                        break;
                    case 2: // Down
                        if (y < Map.height - 1 && map.labyrinth[y + 1,x] != '#') y++;
                        break;
                    case 3: // Left
                        if (x > 0 && map.labyrinth[y, x - 1] != '#') x--;
                        break;
                }

                // If the new position is not a wall, break the loop
                if (map.labyrinth[y, x] != '#') break;
            }

            OnStep?.Invoke();

            Console.SetCursorPosition(x, y);
            Console.Write(Body);
            Thread.Sleep(timeForStep);
        }


        public abstract void Move();
    }
}
