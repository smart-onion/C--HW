internal class Program
{
    delegate int MyDelegate(int[] arr, int start, int end);

    private static void Main(string[] args)
    {
        //LoadURLTask();
        HW3();
    }

    static void LoadURLTask()
    {
        List<string> urls = new List<string>
        {
           "https://google.com",
           "https://apple.com",
           "https://telegram.org"
        };

        CancellationTokenSource cts = new();
        cts.CancelAfter(TimeSpan.FromSeconds(5));
        CancellationToken tocken = cts.Token;

        Task t = Task.Run(() =>
        {
            while(true)
            {
                tocken.ThrowIfCancellationRequested();
                if (Console.ReadKey().KeyChar == 'c')
                {
                    cts.Cancel();
                }
            }
            
        }, tocken);

        Parallel.ForEach(urls, async url =>
        {
           await LoadURL(url, tocken);
        });

        try
        {
            t.Wait();

        }
        catch (Exception)
        {

            Console.WriteLine("Task wa canceled");
        }

    }
    static async Task LoadURL(string url, CancellationToken token)
    {
        using HttpClient client = new HttpClient();

        List<Task> downloadTasks = new List<Task>();


        await Task.Run(async () =>
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url, token);
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Downloaded content from {url}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Download from {url} was cancelled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading from {url}: {ex.Message}");
            }
        }, token);
        

        await Task.WhenAll(downloadTasks);
    }

    static void HW2()
    {
        int[] array = new int[100];

        int taskNumber = 4;
        Task<int>[] tasks = new Task<int>[taskNumber];

        int chunkSize = array.Length / taskNumber;

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        MyDelegate func = (int[] arr, int start, int end) =>
        {
            int sum = 0;
            for (int i = start; i < end - 1; i++) sum += arr[i];
            return sum;
        };


        for (int i = 0; i < taskNumber; i++)
        {
            int start = i * chunkSize;
            int end = start + chunkSize;
            tasks[i] = Task.Run(() => func(array, start, end));
        }


        Task.WaitAll(tasks);
        int result = tasks.Sum(t => t.Result);
        Console.WriteLine("Sum = " + result);
        Console.ReadLine();
        
    }

    static void HW3()
    {
        int[] a = [4,2,3,2,12,3,4,234,123,32,4,123,323,4,1,1,1,2,3,2,2,1,2,3,2,1,4,5,4,3,2,34,];
        int searcIndex = 0;
        Task removeSame = Task<int>.Run(() => {  a = a.ToHashSet().ToArray(); });
        Task sort = removeSame.ContinueWith(t => 
        { a = a.Order().ToArray(); });
        Task<int> search = sort.ContinueWith(t => { return a.ToList().BinarySearch(2); });

        removeSame.Wait();
        Console.WriteLine("Resulr = " + search.Result);

    }

}