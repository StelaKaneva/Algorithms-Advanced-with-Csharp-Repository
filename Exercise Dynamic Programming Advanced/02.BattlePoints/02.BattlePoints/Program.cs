using System;
using System.Linq;

namespace _02.BattlePoints
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var energy = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            var battlePoints = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            var maxEnergy = int.Parse(Console.ReadLine());

            var dp = new int[energy.Length + 1, maxEnergy + 1];

            for (int enemyIdx = 1; enemyIdx < dp.GetLength(0); enemyIdx++)
            {
                var enemyEnergy = energy[enemyIdx - 1];
                var enemyBattlePoints = battlePoints[enemyIdx - 1];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    var excluding = dp[enemyIdx - 1, capacity];

                    if (capacity < enemyEnergy)
                    {
                        dp[enemyIdx, capacity] = excluding;
                        continue;
                    }

                    var including = enemyBattlePoints + dp[enemyIdx - 1, capacity - enemyEnergy];

                    dp[enemyIdx, capacity] = Math.Max(including, excluding);
                }
            }

            Console.WriteLine(dp[energy.Length, maxEnergy]);
        }
    }
}
