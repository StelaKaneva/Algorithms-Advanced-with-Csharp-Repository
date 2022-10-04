using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Boxes
{
    public class Box
    {
        public Box(string box)
        {
            var args = box.Split()
                           .Select(int.Parse)
                           .ToArray();

            this.Width = args[0];
            this.Depth = args[1];
            this.Height = args[2];
        }

        public int Width { get; set; }

        public int Depth { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return $"{this.Width} {this.Depth} {this.Height}";
        }

        public bool IsBigger(Box other)
        {
            return this.Width > other.Width && this.Height > other.Height && this.Depth > other.Depth;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberOfBoxes = int.Parse(Console.ReadLine());
            var boxes = new List<Box>();

            for (int i = 0; i < numberOfBoxes; i++)
            {
                boxes.Add(new Box(Console.ReadLine()));
            }

            var lis = FindLis(boxes);

            foreach (var Box in lis)
            {
                Console.WriteLine(Box.ToString());
            }
        }

        private static IEnumerable<Box> FindLis(List<Box> boxes)
        {
            var prevIndex = new int[boxes.Count];
            var length = new int[boxes.Count];

            var bestLength = 0;
            var startingIndex = 0;

            for (int currentIndex = 0; currentIndex < boxes.Count; currentIndex++)
            {
                var currentBox = boxes[currentIndex];
                var currentBoxLength = 1;
                var currentParent = -1;

                for (int previousBoxIndex = currentIndex - 1; previousBoxIndex >= 0; previousBoxIndex--)
                {
                    var previousBox = boxes[previousBoxIndex];
                    var previousBoxLength = length[previousBoxIndex];

                    if (currentBox.IsBigger(previousBox) &&
                        currentBoxLength <= previousBoxLength + 1)
                    {
                        currentBoxLength = previousBoxLength + 1;
                        currentParent = previousBoxIndex;
                    }
                }

                prevIndex[currentIndex] = currentParent;
                length[currentIndex] = currentBoxLength;

                if (currentBoxLength > bestLength)
                {
                    bestLength = currentBoxLength;
                    startingIndex = currentIndex;
                }
            }

            return ReconstructLis(startingIndex, boxes, prevIndex);
        }

        private static IEnumerable<Box> ReconstructLis(int startingIndex, List<Box> boxes, int[] prevBoxIndex)
        {
            var lis = new Stack<Box>();

            while (startingIndex != -1)
            {
                lis.Push(boxes[startingIndex]);
                startingIndex = prevBoxIndex[startingIndex];
            }

            return lis;
        }
    }
}

