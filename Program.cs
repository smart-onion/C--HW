internal class Program
{
    private static async Task Main(string[] args)
    {
        // Task 1
        //string text = await ReadFromFileAsync("");
        //Console.WriteLine(text.Length);

        // Task 2
        //var pathToDownload = "https://fs1.itstep.org/api/v1/files/mp0Kr-U5o3Wi7YpmPH0bDkgvPAtQ452W?inline=true";
        //DonwlaodFileAsync(pathToDownload, "test2.docx");
        //Console.ReadKey();

        // Task 3
        GetPrimeNumbers(100);
        Console.WriteLine("Simulate another job");
        Console.ReadKey();
    }

    static async Task<string> ReadFromFileAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }

    static async void DonwlaodFileAsync(string pathToFile, string destionationPath)
    {
        using var client = new HttpClient();
        var file = await client.GetAsync(pathToFile);


        FileStream fileStream = new FileStream(destionationPath, FileMode.Create, FileAccess.Write);
        await file.Content.CopyToAsync(fileStream );
        Console.WriteLine("File downloaded successfully!");
    }

    static async void GetPrimeNumbers(int maxNumber)
    {
        var arr = await GeneratePrimeNumberAsync(maxNumber);
    
        foreach (var number in arr)
        {
            Console.Write(number + " ");
        }
    }

    static async Task<List<int>> GeneratePrimeNumberAsync(int maxNumber)
    {
        return await Task.Run(() =>
        {
            List<int> primeNumbers = new();

            for (int i = 2; i <= maxNumber; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers;
        });
    }
}