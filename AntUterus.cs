namespace Anthill
{
    public class AntUterus : Ant
    {
        private readonly int maxFill = 100;
        private readonly int threadSleep= 10000;
        private static readonly int age = 1000;

        public AntUterus() : base('U', age) 
        {

        }
        public override void Move()
        {
            while (Age > 0)
            {
                Thread.Sleep(threadSleep);
                Spawn();
                Reproduction(); 
                Age--;
            }
        }

        void Reproduction()
        {
            lock (AntIntelligence.ants)
            {
                AntIntelligence.ants.Add(new AntScout());
                Thread.Sleep(threadSleep);
                AntIntelligence.ants.Add(new AntWorker());
            }
        }

        protected override void Spawn() 
        {
            x = AntIntelligence.homePosition.Item2;
            y = AntIntelligence.homePosition.Item1;
            Console.SetCursorPosition(x, y);
            Console.Write(Body);
        }
    }
}
