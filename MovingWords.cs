namespace HW4
{
    public class MovingWords
    {
        static object locker = new object();
        (int, int) startPosition;
        bool isForward;
        string word;
        public MovingWords(bool isForward, string word)
        {
            this.isForward = isForward;
            this.word = word;
        }

        public void Move()
        {
            var spaces = () =>
            {
                var str = " ";
                foreach (var item in word)
                {
                    str += " ";
                }
                return str;
            };
                if (isForward)
                {
                    startPosition = (1, 0);
                    while (startPosition.Item1 < Console.WindowWidth - word.Length)
                    {
                        lock (locker)
                    {
                        
                        Console.SetCursorPosition(startPosition.Item1 - 1, startPosition.Item2);
                        Console.Write(spaces());
                        Console.SetCursorPosition(startPosition.Item1++, startPosition.Item2);
                        Console.Write(word);
                        Thread.Sleep(50);
                    }
                        
                    }
                }
                else
                {
                    startPosition = (Console.WindowWidth - word.Length, 2);
                    while (startPosition.Item1 > 0)
                    {
                        lock(locker)
                    {
                        Console.SetCursorPosition(startPosition.Item1, startPosition.Item2);
                        Console.Write(spaces());
                        Console.SetCursorPosition(startPosition.Item1--, startPosition.Item2);
                        Console.Write(word);
                        Thread.Sleep(50);
                    }
                        
                    }
                }
            }
        
    }
}
