namespace Anthill
{
    public class AntScout : Ant
    {
        public AntScout() : base('s', 200) 
        {
            OnStep += () =>
            {
                if (map.labyrinth[y,x] == 'F')
                {
                    AntIntelligence.foodPool.Enqueue((y, x));
                }
            };
        }

        public override void Move()
        {
            while (Age > 0)
            {
                NextStep();
                Age--;
            }
            Console.SetCursorPosition(x,y);
            Console.Write("x");
        }
    }
}
