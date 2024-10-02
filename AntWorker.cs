namespace Anthill
{
    class AntWorker : Ant
    {
        public (int, int) HomeCoord { get; set; }
        public (int, int) FoodCoord { get; set; }

        public event Func<bool> OnFoodFound;
        private AStarPathfinder pathfinder = new AStarPathfinder(map.labyrinth);
        public AntWorker() : base('w', 100) 
        {
            OnFoodFound = () => false;
        }
        public override async void Move()
        {
            while (Age > 0)
            {
                if (AntIntelligence.foodPool.Count > 0)
                {
                    MoveTo(AntIntelligence.foodPool.Dequeue(), () => 
                    {
                        if (map.labyrinth[y,x] == 'F')
                        {
                            return true;
                        }
                        return false;
                    });

                    MoveTo(AntIntelligence.homePosition);

                    Age--;
                }
            }
        }

        private void MoveTo((int, int) position, Func<bool>? action = null)
        {
            List<(int, int)> path = pathfinder.FindPath((y,x), position);
            if (action == null) action = () => false;
            FollowPath(path, action);
        }

        void FollowPath(List<(int,int)> path, Func<bool> action)
        {
            foreach (var step in path)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
                x = step.Item2;
                y = step.Item1;

                Console.SetCursorPosition(x, y);
                Console.Write(Body);
                Thread.Sleep(timeForStep);
                if (action.Invoke()) break;

            }
        }
    }
}
