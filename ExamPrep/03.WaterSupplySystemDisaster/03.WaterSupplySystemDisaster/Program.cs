﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.WaterSupplySystemDisaster
{
    internal class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] lowpoint;
        private static int[] depths;
        private static int[] parent;
        private static List<int> articulationPoints;

        static void Main(string[] args)
        {
            var nodeCount = int.Parse(Console.ReadLine());
            var targetPartsCount = int.Parse(Console.ReadLine());

            ReadGraph(nodeCount);

            visited = new bool[graph.Length];
            parent = new int[graph.Length];
            lowpoint = new int[graph.Length];
            depths = new int[graph.Length];
            articulationPoints = new List<int>();

            for (int node = 0; node < parent.Length; node++)
            {
                parent[node] = -1;
            }

            for (int node = 1; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                }
            }

            foreach (var point in articulationPoints)
            {
                var countOfComponents = 0;

                visited = new bool[graph.Length];

                for (int node = 1; node < graph.Length; node++)
                {
                    if (node == point || visited[node])
                    {
                        continue;
                    }

                    DFS(node, point);
                    countOfComponents += 1;
                }

                if (countOfComponents == targetPartsCount)
                {
                    Console.WriteLine(point);
                    return;
                }
            }

            Console.WriteLine(0);
        }

        private static void DFS(int node, int point)
        {
            if (visited[node] || node == point)
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, point);
            }
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            lowpoint[node] = depth;
            depths[node] = depth;

            var childCount = 0;
            var isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parent[child] = node;

                    FindArticulationPoints(child, depth + 1);

                    childCount += 1;

                    if (lowpoint[child] >= depth)
                    {
                        isArticulationPoint = true;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);
                }
                else if (parent[node] != child)
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depths[child]);
                }
            }

            if (parent[node] == -1 && childCount > 1 ||
                parent[node] != -1 && isArticulationPoint)
            {
                articulationPoints.Add(node);
            }
        }

        private static void ReadGraph(int nodeCount)
        {
            graph = new List<int>[nodeCount + 1];

            for (int i = 1; i < graph.Length; i++)
            {
                graph[i] = new List<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            }
        }
    }
}
