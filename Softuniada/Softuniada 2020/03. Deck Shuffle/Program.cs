using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Deck_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<int> arr = Enumerable.Range(1, n).ToList();

            int[] ranges = Console.ReadLine()
                                  .Split()
                                  .Select(int.Parse)
                                  .ToArray();

            for (int i = 0; i < ranges.Length; i++)
            {
                int currentIndex = ranges[i];

                List<int> firstDeck = arr.Take(currentIndex).ToList();

                List<int> secondDeck = arr.Skip(currentIndex).Take(arr.Count).ToList();

                List<int> result = new List<int>();
                
                while (firstDeck.Count != 0 && secondDeck.Count != 0)
                {
                    result.Add(firstDeck[0]);
                    firstDeck.RemoveAt(0);
                    result.Add(secondDeck[0]);
                    secondDeck.RemoveAt(0);
                }

                if(firstDeck.Count != 0)
                {
                    result.AddRange(firstDeck);
                }
                else
                {
                    result.AddRange(secondDeck);
                }

                arr = result;

            }

            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
