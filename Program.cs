using System;
using System.Collections.Generic;
using Anthill;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        Map map = new Map();
        Ant.AsignMap(map);
        Dictionary<Ant,Thread> threads = new();
        AntIntelligence.ants.AddRange(new List<Ant>
        {
            new AntScout(),
            new AntScout(),
            new AntWorker(),
            new AntUterus(),
        });

        while (true)
        {
            for (int i = 0; i < AntIntelligence.ants.Count; i++)
            {
                if (!threads.ContainsKey(AntIntelligence.ants[i]))
                {
                    threads[AntIntelligence.ants[i]] = new Thread(AntIntelligence.ants[i].Move);
                }
                else
                {
                    if (!threads[AntIntelligence.ants[i]].IsAlive) threads[AntIntelligence.ants[i]].Start();
                }
            }
            Thread.Sleep(1000);

        }

    }
}
