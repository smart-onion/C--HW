using System.Text.RegularExpressions;

internal class Program
{

    private static void Main(string[] args)
    {
        String text = "RegExr was  by gskinner.com.Edit the Expression & Text to see matches. Roll over matches or the expression for details. PCRE & JavaScript flavors of RegEx are supported. Validate your e xpression with Tests mode.The side bar includes a Cheatsheet, full Reference, and Help. You can also Save & Share with the Community and view patterns you create or favorite in My Patterns.\r\nsdccddcccd\r\nExplore results with the Tools below. Replace & List output custom results. Details lists capture groups. Explain describes your expression in plain ";
        Regex regex = new Regex(@"\b\w*do\w*\b|\b\w*re\w*\b|\b\w*mi\w*\b|\b\w*fa\w*\b|\b\w*sol\w*\b|\b\w*la\w*\b|\b\w*si\w*\b");

        MatchCollection matches = regex.Matches(text);

        foreach (Match match in matches)
        {
            Console.WriteLine(match);
        }
   

        String text2 = "RegExr was created by gskinner.com.\r\n\r\nEdit the Expression 11:11 & Text to see matches. Roll over matches or the expression for details. PCRE & JavaScript flavors of RegEx are supported. Validate your e xpression with Tests mode.\r\n\r\nThe side bar includes a Cheatsheet, full Reference, and 12:30 Help. You can also Save & Share with the Community and view patterns you create or favorite in My Patterns.\r\nsdccddcccd\r\nExplore results with the Tools below. Replace & List output custom results. Details lists capture groups. Explain describes your expression in plain \r\n\r\n14:00case";

        Regex regex2 = new Regex(@"\d+:\d+");

        MatchCollection matches2 = regex2.Matches(text2);

        foreach (Match match in matches2)
        {
            Console.WriteLine(match);
        }

    }
}