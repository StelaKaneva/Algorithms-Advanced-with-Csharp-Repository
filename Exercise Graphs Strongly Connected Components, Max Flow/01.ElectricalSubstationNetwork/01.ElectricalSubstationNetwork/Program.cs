using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.ElectricalSubstationNetwork
{
    internal class Program
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var lines = int.Parse(Console.ReadLine());

            graph = new List<int>[nodes];
            reversedGraph = new List<int>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
                reversedGraph[node] = new List<int>();
            }

            for (int l = 0; l < lines; l++)
            {
                var lineElements = Console.ReadLine()
                            .Split(", ")
                            .Select(int.Parse)
                            .ToArray();

                var node = lineElements[0];

                for (int i = 1; i < lineElements.Length; i++)
                {
                    var child = lineElements[i];

                    graph[node].Add(child);
                    reversedGraph[child].Add(node);
                }
            }

            var sorted = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                DFS(node, graph, visited, sorted);
            }

            visited = new bool[graph.Length];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                DFS(node, reversedGraph, visited, component);

                Console.WriteLine(String.Join(", ", component));
            }
        }

        private static void DFS(int node, List<int>[] targetGraph, bool[] visited, Stack<int> stack)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in targetGraph[node])
            {
                DFS(child, targetGraph, visited, stack);
            }

            stack.Push(node);
        }
    }
}
