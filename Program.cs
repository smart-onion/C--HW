using HW4;

internal class Program
{
    public static event Func<bool> OnEvent;
    static object locker = new(); 
    private static void Main(string[] args)
    {
        // task 1
        //OnEvent += () => true;
        //BankTask();
        // task 2
        MovingWords fwd = new MovingWords(true, "fwd");
        MovingWords back = new MovingWords(false, "back");
        Thread fwdThread = new Thread(fwd.Move);
        Thread backThread = new Thread(back.Move);
        fwdThread.Start();
        backThread.Start();
    }

    private static void BankTask()
    {

        Thread[] clients = new Thread[5];

        for (int i = 0; i < clients.Length; i++)
        {
            clients[i] = new Thread(new Client().ReceiveMoney);
            clients[i].Start();
        }
        Thread.Sleep(20000);
        OnEvent += () => false;
    }

    class Bank
    {
        protected static int bankAmount = 1000;
        protected bool DestributeCash(int amount)
        {
            if (amount <= bankAmount)
            {
                bankAmount -= amount;
                return true;
            }
            return false;
        }
    }

    class ATM : Bank
    {
        public int GetCash(int amount)
        {
            if (DestributeCash(amount))
            {
                Console.Write($"Clent {Thread.CurrentThread.ManagedThreadId} received {amount}$\t Bank balance: {bankAmount} ");
                return amount;
            }
            Console.Write($"Client {Thread.CurrentThread.ManagedThreadId}. Opp... money exeed bank balance {bankAmount} ");
            return 0;
        }
    }

    class Client
    {
        public int balance;
        Random rand = new Random();
        ATM atm = new ATM();
        public void ReceiveMoney()
        {
            while (OnEvent.Invoke())
            {
                lock (locker)
                {
                    int time = rand.Next(1000, 2000);
                    Thread.Sleep(time);
                    balance += atm.GetCash(time / 10);
                    Console.WriteLine("\t\tClient Balance: " + balance);
                }
            }
        }
    }


}