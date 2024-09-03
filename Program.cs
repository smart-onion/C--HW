using HW3;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new Application();
        app.Run();
    }

    public static void ShowTable<T>(List<T> list)
    {
        Type objType = typeof(T);
        foreach (var item in list)
        {
            foreach (var eq in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.Write(eq.GetValue(item) + " ");
            }
            Console.WriteLine();
        }
    }
}