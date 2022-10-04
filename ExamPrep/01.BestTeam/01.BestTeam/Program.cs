using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.BestTeam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var soldiers = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            var lis = FindLis(soldiers).Reverse().ToArray();

            var reversedSoldiers = soldiers.Reverse().ToArray();
            var lds = FindLis(reversedSoldiers).ToArray();

            if (lis.Length > lds.Length)
            {
                Console.WriteLine(String.Join(" ", lis));
                return;
            }

            Console.WriteLine(String.Join(" ", lds));
        }

        private static ICollection<int> FindLis(int[] soldiers)
        {
            var prevSoldierIndex = new int[soldiers.Length];
            var soldiersLength = new int[soldiers.Length];

            var bestLength = 0;
            var startingIndex = 0;

            for (int currentSoldierIndex = 0; currentSoldierIndex < soldiers.Length; currentSoldierIndex++)
            {
                var currentSoldier = soldiers[currentSoldierIndex];
                var currentSoldierLength = 1;
                var currentParent = -1;
               
                for (int previousSoldierIndex = currentSoldierIndex - 1; previousSoldierIndex >= 0; previousSoldierIndex--)
                {
                    var previousSoldier = soldiers[previousSoldierIndex];
                    var previousSoldierLength = soldiersLength[previousSoldierIndex];

                    if (previousSoldier < currentSoldier &&
                        currentSoldierLength <= previousSoldierLength + 1)
                    {
                        currentSoldierLength = previousSoldierLength + 1;
                        currentParent = previousSoldierIndex;
                    }
                }

                prevSoldierIndex[currentSoldierIndex] = currentParent;
                soldiersLength[currentSoldierIndex] = currentSoldierLength;

                if (currentSoldierLength > bestLength)
                {
                    bestLength = currentSoldierLength;
                    startingIndex = currentSoldierIndex;
                }
            }

            return ReconstructLis(startingIndex, soldiers, prevSoldierIndex);
        }

        private static ICollection<int> ReconstructLis(int startingIndex, int[] soldiers, int[] prevSoldierIndex)
        {
            var lis = new List<int>();

            while (startingIndex != -1)
            {
                lis.Add(soldiers[startingIndex]);
                startingIndex = prevSoldierIndex[startingIndex];
            }

            return lis;
        }
    }
}
