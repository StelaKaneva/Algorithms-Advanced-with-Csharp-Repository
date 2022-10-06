﻿using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.TourDeSofia
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var startNode = int.Parse(Console.ReadLine());

            var graph = new List<Edge>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                var edgeParts = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

                var from = edgeParts[0];
                var to = edgeParts[1];
                var weight = edgeParts[2];

                var edge = new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                };

                graph[from].Add(edge);
            }

            var distance = new double[graph.Length];

            for (int node = 0; node < distance.Length; node++)
            {
                distance[node] = double.PositiveInfinity;
            }

            var bag = new OrderedBag<int>(
                        Comparer<int>.Create((f, s) => distance[f].CompareTo(distance[s])));

            foreach (var edge in graph[startNode])
            {
                distance[edge.To] = edge.Weight;
                bag.Add(edge.To);
            }

            while (bag.Count > 0)
            {
                var minNode = bag.RemoveFirst();

                if (minNode == startNode)
                {
                    break;
                }

                foreach (var edge in graph[minNode])
                {
                    if (double.IsPositiveInfinity(distance[edge.To]))
                    {
                        bag.Add(edge.To);
                    }

                    var newDistance = edge.Weight + distance[minNode];

                    if (newDistance < distance[edge.To])
                    {
                        distance[edge.To] = newDistance;

                        bag = new OrderedBag<int>(
                        bag,
                        Comparer<int>.Create((f, s) => distance[f].CompareTo(distance[s])));
                    }
                }
            }

            if (double.IsPositiveInfinity(distance[startNode]))
            {
                Console.WriteLine(distance.Count(x => !double.IsPositiveInfinity(x)) + 1);
            }
            else
            {
                Console.WriteLine(distance[startNode]);
            }
        }
    }
}
