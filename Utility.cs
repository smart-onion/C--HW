using System.Reflection;

namespace HW61
{
    public static class Utility
    {
        public static int GetInt()
        {
            int userInput;

            while (!int.TryParse(Console.ReadLine(), out userInput)) { }

            return userInput;
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


}
