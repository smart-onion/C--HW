using System;
using System.Collections.Generic;

public class AStarPathfinder
{
    private char[,] map;
    private int width;
    private int height;

    public AStarPathfinder(char[,] map)
    {
        this.map = map;
        this.width = map.GetLength(0);
        this.height = map.GetLength(1);
    }

    public List<(int, int)> FindPath((int, int) start, (int, int) goal)
    {
        var openList = new List<Node>();
        var closedList = new HashSet<(int, int)>();

        Node startNode = new Node(start, null, 0, GetHeuristic(start, goal));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.F.CompareTo(b.F));
            Node currentNode = openList.ElementAt(0);
            openList.RemoveAt(0);

            if (currentNode.Position == goal)
            {
                return ReconstructPath(currentNode);
            }

            closedList.Add(currentNode.Position);

            foreach (var neighbor in GetNeighbors(currentNode.Position))
            {
                if (map[neighbor.Item1, neighbor.Item2] == '#' || closedList.Contains(neighbor))
                {
                    continue;
                }

                int tentativeG = currentNode.G + 1;
                Node neighborNode = new Node(neighbor, currentNode, tentativeG, GetHeuristic(neighbor, goal));

                if (!openList.Exists(n => n.Position == neighbor && n.G <= tentativeG))
                {
                    openList.Add(neighborNode);
                }
            }
        }

        return new List<(int, int)>();
    }

    private List<(int, int)> ReconstructPath(Node node)
    {
        var path = new List<(int, int)>();
        while (node != null)
        {
            path.Add(node.Position);
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }

    private int GetHeuristic((int, int) a, (int, int) b)
    {
        return Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
    }

    private List<(int, int)> GetNeighbors((int, int) position)
    {
        var neighbors = new List<(int, int)>();

        var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        foreach (var direction in directions)
        {
            int newX = position.Item1 + direction.Item1;
            int newY = position.Item2 + direction.Item2;

            if (newX >= 0 && newX < width && newY >= 0 && newY < height)
            {
                neighbors.Add((newX, newY));
            }
        }

        return neighbors;
    }

    private class Node
    {
        public (int, int) Position { get; }
        public Node Parent { get; }
        public int G { get; }
        public int H { get; }
        public int F => G + H;

        public Node((int, int) position, Node parent, int g, int h)
        {
            Position = position;
            Parent = parent;
            G = g;
            H = h;
        }
    }
}
