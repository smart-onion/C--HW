using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anthill
{
    public static class AntIntelligence
    {
        public static Queue<(int,int)> foodPool = new Queue<(int, int)>();
        public static (int, int) homePosition = (20,20);
        public static List<Ant> ants = new List<Ant>();

    }
}
