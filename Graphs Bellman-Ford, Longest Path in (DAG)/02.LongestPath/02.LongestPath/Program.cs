using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.LongestPath
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            edgesByNode = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];

                if (!edgesByNode.ContainsKey(from))
                {
                    edgesByNode.Add(from, new List<Edge>());
                }

                if (!edgesByNode.ContainsKey(to))
                {
                    edgesByNode.Add(to, new List<Edge>());
                }

                edgesByNode[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = edgeData[2]
                });
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distance = new double[nodes + 1];
            Array.Fill(distance, double.NegativeInfinity);
            distance[source] = 0;

            var prev = new int[nodes + 1];
            Array.Fill(prev, -1);

            var sortedNodes = TopologicalSorting();

            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in edgesByNode[node])
                {
                    var newDistance = distance[edge.From] + edge.Weight;

                    if (newDistance > distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                    }
                }
            }

            Console.WriteLine(distance[destination]);

            var currentNode = destination;

            var path = new Stack<int>();

            while (currentNode != -1)
            {
                path.Push(currentNode);
                currentNode = prev[currentNode];
            }

            Console.WriteLine(String.Join(" ", path));
        }

        private static Stack<int> TopologicalSorting()
        {
            var result = new Stack<int>();
            var visited = new HashSet<int>();

            foreach (var node in edgesByNode.Keys)
            {
                DFS(node, visited, result);
            }

            return result;
        }

        private static void DFS(int node, HashSet<int> visited, Stack<int> result)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var edge in edgesByNode[node])
            {
                DFS(edge.To, visited, result);
            }

            result.Push(node);
        }
    }
}
