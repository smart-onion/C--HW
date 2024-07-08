
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
public class Program
 {
    public static void Main()
    {
        DirectoryInfo root = new DirectoryInfo("G:/Games");
        FileInfo[] files = root.GetFiles("*", SearchOption.AllDirectories);

        Dictionary<string, long[]> dict = new Dictionary<string, long[]>();

        long totalSize = 0;
        long totalCount = 0;

        foreach (FileInfo item in files)
        {
            if (dict.ContainsKey(item.Extension))
            {
                dict[item.Extension][0]++;
                dict[item.Extension][1] += item.Length;
            }
            else
            {
                if (item.Extension.Equals("")) continue;

                dict.Add(item.Extension, new long[2] { 1, item.Length });
            }
            totalSize += item.Length;
            totalCount++;
        }

        int number = 1;
        Console.WriteLine("+----+---------------------+---------+---------------------+------------------+------------------+");
        Console.WriteLine("| No | Extension           | Count   | Total Size in Bytes | % of total files | % of total bytes |");
        Console.WriteLine("+----+---------------------+---------+---------------------+------------------+------------------+");

        foreach (var item in dict.OrderBy(v => v.Value[0]).Reverse().Take(50))
        {
            double percentOfCount = (item.Value[0] / (float)totalCount) * 100;
            double percentOfSize = (item.Value[1] / (float)totalSize) * 100;

            Console.WriteLine("| {0,-2} | {1,-19} | {2,-7} | {3,-19} | {4,-16} | {5,-16} |", number, item.Key, item.Value[0], item.Value[1], Math.Round(percentOfCount, 2), Math.Round(percentOfSize, 2));
            number++;
        }

        Console.WriteLine("+----+---------------------+---------+---------------------+------------------+------------------+");
        Console.WriteLine("| TOTAL:                   | {0,-7} | {1,-19} | {2, -16} | {3, -16} |", totalCount, totalSize, 100, 100);
        Console.WriteLine("+--------------------------+---------+---------------------+------------------+------------------+");

    }
}
