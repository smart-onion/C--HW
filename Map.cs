namespace Anthill
{
    public class Map
    {
        public static int width;
        public static int height;
        public char[,] labyrinth;

        public event Func<bool> OnUpdate;

        public Map() 
        {
            OnUpdate += () => true;
            width = 81; // Must be odd
            height = 31; // Must be odd
            labyrinth = new char[height, width];
            Initialize();
            //OnUpdate += () => false;
        }

        public void Initialize()
        {
            
            Random rand = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1) labyrinth[y, x] = '#';
                    else labyrinth[y, x] = ' ';

                }
            } 

            // Add a few walls randomly
            int numberOfWalls = (width * height) / 20; 
            for (int i = 0; i < numberOfWalls; i++)
            {
                int x = rand.Next(1, width - 1);
                int y = rand.Next(1, height - 1);
                labyrinth[y, x] = '#';
            }

            // Updating map
            if (OnUpdate.Invoke())
            {
                // Add a few walls randomly
                int numberOfFood = 40;
                for (int i = 0; i < numberOfFood; i++)
                {
                    int x = rand.Next(1, width - 1);
                    int y = rand.Next(1, height - 1);
                    labyrinth[y, x] = 'F';
                }
            }

            // Display the labyrinth
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(labyrinth[y, x]);
                }
                Console.WriteLine();
            }

            
        }
    }
}
