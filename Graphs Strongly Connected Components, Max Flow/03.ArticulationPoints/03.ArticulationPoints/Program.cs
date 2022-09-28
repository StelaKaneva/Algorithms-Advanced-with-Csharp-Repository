using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.ArticulationPoints
{
    internal class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] depth;
        private static int[] lowpoint;
        private static int?[] parent;

        private static List<int> articulationPoints;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var lines = int.Parse(Console.ReadLine());

            graph = new List<int>[nodes];
            visited = new bool[nodes];
            depth = new int[nodes];
            lowpoint = new int[nodes];
            parent = new int?[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }

            for (int l = 0; l < lines; l++)
            {
                var line = Console.ReadLine()
                           .Split(", ")
                           .Select(int.Parse)
                           .ToArray();

                var node = line[0];

                for (int j = 1; j < line.Length; j++)
                {
                    var child = line[j];

                    graph[node].Add(child);
                    graph[child].Add(node);
                }
            }

            articulationPoints = new List<int>();

            for (int node = 0; node < graph.Length; node++)
            {
                if (visited[node])
                {
                    continue;
                }

                FindArticulationPoints(node, 1);
            }

            Console.WriteLine($"Articulation points: {String.Join(", ", articulationPoints)}");
        }

        private static void FindArticulationPoints(int node, int currentDepth)
        {
            visited[node] = true;

            depth[node] = currentDepth;
            lowpoint[node] = currentDepth;

            var childCount = 0;
            var isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parent[child] = node;
                    FindArticulationPoints(child, currentDepth + 1);

                    childCount += 1;

                    if (lowpoint[child] >= depth[node])
                    {
                        isArticulationPoint = true;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);
                }
                else if (parent[node] != child)
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depth[child]);
                }
            }

            if ((parent[node] == null && childCount > 1) || 
                (parent[node] != null && isArticulationPoint))
            {
                articulationPoints.Add(node);
            }
        }
    }
}
