using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

internal class Program
{
    static HttpClient httpClient = new();
    private static async Task Main(string[] args)
    {
        await Task3();
    }

    static void Task0()
    {
        var web = new WebClient();

        Uri uri = new Uri("https://yt3.googleusercontent.com/viNp17XpEF-AwWwOZSj_TvgobO1CGmUUgcTtQoAG40YaYctYMoUqaRup0rTxxxfQvWw3MvhXesw=s900-c-k-c0x00ffffff-no-rj");

        web.DownloadFile(uri, "test.png");

    }

    static async Task Task1()
    {
        var request = await httpClient.GetAsync("https://google.com");
        var respont = request.Content;
        foreach (var header in respont.Headers)
        {
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        Console.WriteLine($"Status code: {request.StatusCode}");
        httpClient.Dispose();
    }

    static async Task Task2()
    {
        Console.Write("Enter url to read: ");
        Uri? uri = new Uri(Console.ReadLine());
        var tempFile = "test.txt";
        var html = await httpClient.GetAsync(uri);
        var responce = await html.Content.ReadAsStringAsync();

        var file = File.Create(tempFile);
        var data = Encoding.UTF8.GetBytes(responce);
        await file.WriteAsync(data, 0, data.Length);
        file.Close();
        Process process = new Process();
        process.StartInfo.FileName = "notepad.exe";
        process.StartInfo.Arguments = tempFile;
        process.Start();

        

    }

    static async Task Task3()
    {
        Uri uri = new Uri("https://www.gutenberg.org/cache/epub/27761/pg27761-images.html");
        var request = await httpClient.GetAsync(uri);
        var responce = await request.Content.ReadAsStringAsync();

        string pattern = @">([^<]+)<";
        MatchCollection matches = Regex.Matches(responce, pattern);
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Groups[1].Value);
        }
    }
}