internal class Program
{
    private static void Spiral()
    {
        int N = 10;

        int[,] arr = new int[N, N];

        int[][] directions = new int[][]
        {
            new int[] {0, -1}, // Up
            new int[] {0, 1}, // Down
            new int[] {1, 0}, // Right
            new int[] {-1, 0}, // left
        };

        int step = 1;

        int number = 1;

        int x = N / 2;
        int y = x;
        arr[y,x] = number++;

        while (number < N * N)
        {
            for (int i = 0; i < 2; i++)
            {

            }
        }
    }

    static void Task2()
    {
        int rows = 10;
        int colms = 6;
        int[,] matrix = new int[rows, colms];

        int number = 1;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colms; x++)
            {
                matrix[y, x] = number++;
                Console.Write(matrix[y, x] + " ");
            }
            Console.WriteLine();
        }
    }

    static void Task3()
    {
        int rows = 10;
        int colms = 6;
        int[,] matrix = new int[rows, colms];

        int number = 1;

        for (int y = 0; y < rows; y++)
        {
            if (y % 2 != 0)
            {
                for (int x = colms - 1; x >= 0; x--)
                {
                    matrix[y, x] = number++;
                }
            }
            else
            {
                for (int x = 0; x < colms; x++)
                {
                    matrix[y, x] = number++;
                }
            }

        }

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colms; x++)
            {
                Console.Write(matrix[y, x] + " ");
            }
            Console.WriteLine();
        }

    }

    static void Task4()
    {
        int size = 20;

        int radius = size / 2;

        int[,,] matrix = new int[size, size, size];

        for (int z = 0, a = - radius; z < size; z++, a++)
        {
            for (int y = 0, b = - radius; y < size; y++, b++)
            {
                for (int x = 0, c = -radius; x < size; x++, c++)
                {
                    if (a*a + b*b + c*c <= radius*radius)
                    {
                        matrix[z, y, x] = 1;
                        Console.Write("*");
                    }
                    else
                    {
                        matrix[z, y, x] = 0;
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    private static void Main(string[] args)
    {
        Task4();
    }
}