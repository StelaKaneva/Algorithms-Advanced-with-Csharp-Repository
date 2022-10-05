using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.RoadTrip
{
    public class Item
    {
        public int Weight { get; set; }

        public int Value { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Item>();

            var itemsValue = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var itemsWeight = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

            var maxCapacity = int.Parse(Console.ReadLine());

            var itemsCount = itemsValue.Length;

            for (int item = 0; item < itemsCount; item++)
            {
                items.Add(new Item
                {
                    Value = itemsValue[item],
                    Weight = itemsWeight[item]
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

            Console.WriteLine($"Maximum value: {dp[items.Count, maxCapacity]}");
        }
    }
}
