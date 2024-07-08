
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
public class Program
 {
    public static void Main()
    {
        StreamReader sr = new StreamReader("./Kobzar.txt");
        string source = sr.ReadToEnd();
        Regex reg = new Regex(@"\w{3,20}");

        SortedList<string, int> dict = new SortedList<string, int>();

        foreach(Match match in reg.Matches(source))
        {
            string stringMatch = match.ToString().ToLower();


            if (dict.ContainsKey(stringMatch))
            {
                dict[stringMatch]++;
            }
            else
            {
                dict.Add(stringMatch, 1);
            }
        }
        var sortedByCount = dict.OrderBy(v => v.Value).Reverse();

        int number = 1;
        Console.WriteLine("+----+----------------------+-------+");
        Console.WriteLine("| No | Word                 | Count |");
        Console.WriteLine("+----+----------------------+-------+");

        foreach (var item in sortedByCount.Take(50))
        {
            Console.WriteLine("| {0,-2} | {1,-20} | {2,5} |", number, item.Key, item.Value);
            number++;
        }
        Console.WriteLine("+----+----------------------+-------+");

    }
}
