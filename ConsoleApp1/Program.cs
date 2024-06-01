internal class Program
{
    private static void Main(string[] args)
    {
        int stairs = Convert.ToInt32(Console.ReadLine());
        int width = stairs * 2;
        int height = stairs * 2;

        int stepNext = 0;

        for (int y = 0; y < width; y++) 
        { 
            for(int x = 0; x < height; x++) 
            {
                if (y % 2 == 0 && x == stepNext)
                {
                    Console.Write("***");
                    
                }
                if (x == stepNext && y % 2 != 0)
                {
                    Console.Write("*");
                }

                Console.Write(" ");
            }

            if (y % 2 == 0)
            {
                stepNext += 2;
            }

            Console.WriteLine();
        }
    }
}