using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.MostReliablePath
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            var graph = new List<Edge>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                graph[first].Add(edge);
                graph[second].Add(edge);
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var reliability = new double[graph.Length];
            var prev = new int[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                reliability[node] = double.NegativeInfinity;
                prev[node] = -1;
            }

            reliability[source] = 100;

            var bag = new OrderedBag<int>(
                            Comparer<int>.Create((f, s) => reliability[s].CompareTo(reliability[f])));

            bag.Add(source);

            while (bag.Count > 0)
            {
                var node = bag.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    var child = edge.First == node ? edge.Second : edge.First;

                    if (double.IsNegativeInfinity(reliability[child]))
                    {
                        bag.Add(child);
                    }

                    var newReliability = reliability[node] * edge.Weight / 100.0;

                    if (newReliability > reliability[child])
                    {
                        reliability[child] = newReliability;
                        prev[child] = node;

                        bag = new OrderedBag<int>(
                            bag,
                            Comparer<int>.Create((f, s) => reliability[s].CompareTo(reliability[f])));
                    }
                }
            }

            Console.WriteLine($"Most reliable path reliability: {reliability[destination]:F2}%");

            var path = new Stack<int>();

            var currentNode = destination;

            while (currentNode != -1)
            {
                path.Push(currentNode);
                currentNode = prev[currentNode];
            }

            Console.WriteLine(String.Join(" -> ", path));
        }
    }
}
