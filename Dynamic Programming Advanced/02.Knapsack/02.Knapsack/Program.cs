using System;
using System.Collections.Generic;

namespace _02.Knapsack
{
    public class Item
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public int Value { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var maxCapacity = int.Parse(Console.ReadLine());

            var items = new List<Item>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "end")
                {
                    break;
                }

                var itemParts = line.Split();

                items.Add(new Item
                {
                    Name = itemParts[0],
                    Weight = int.Parse(itemParts[1]),
                    Value = int.Parse(itemParts[2]),
                });
            }

            var dp = new int[items.Count + 1, maxCapacity + 1];
            var used = new bool[items.Count + 1, maxCapacity + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                var itemIdx = row - 1;
                var item = items[itemIdx];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    var excluding = dp[row - 1, capacity];

                    if (item.Weight > capacity)
                    {
                        dp[row, capacity] = excluding;
                        continue;
                    }

                    var including = item.Value + dp[row - 1, capacity - item.Weight];

                    if (including > excluding)
                    {
                        dp[row, capacity] = including;
                        used[row, capacity] = true;
                    }
                    else
                    {
                        dp[row, capacity] = excluding;
                    }
                }
            }

            var currentCapacity = maxCapacity;

            var totalWeight = 0;

            var usedItems = new SortedSet<string>();

            for (int row = dp.GetLength(0) - 1; row > 0; row--)
            {
                if (!used[row, currentCapacity])
                {
                    continue;
                }

                var item = items[row - 1];
                totalWeight += item.Weight;
                currentCapacity -= item.Weight;
                usedItems.Add(item.Name);

                if (currentCapacity == 0)
                {
                    break;
                }
            }

            Console.WriteLine($"Total Weight: {totalWeight}");
            Console.WriteLine($"Total Value: {dp[items.Count, maxCapacity]}");
            Console.WriteLine(String.Join(Environment.NewLine, usedItems));
        }
    }
}
