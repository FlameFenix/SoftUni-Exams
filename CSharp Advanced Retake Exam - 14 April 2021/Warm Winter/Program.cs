using System;
using System.Collections.Generic;
using System.Linq;

namespace Warm_Winter
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] inputHats = Console.ReadLine()
                                     .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                     .Select(int.Parse)
                                     .ToArray();

            int[] inputScarfs = Console.ReadLine()
                                     .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                     .Select(int.Parse)
                                     .ToArray();

            Stack<int> hats = new Stack<int>(inputHats);
            Queue<int> scarfs = new Queue<int>(inputScarfs);

            List<int> createdSets = new List<int>();

            while(hats.Count != 0 && scarfs.Count != 0)
            {
                int currentHat = hats.Peek();
                int currentScarf = scarfs.Peek();

                if(currentHat > currentScarf)
                {
                    createdSets.Add(hats.Pop() + scarfs.Dequeue());
                }
                else if(currentHat < currentScarf)
                {
                    hats.Pop();
                    continue;
                }
                else if(currentHat == currentScarf)
                {
                    scarfs.Dequeue();
                    hats.Pop();
                    currentHat += 1;
                    hats.Push(currentHat);
                }
            }

            Console.WriteLine($"The most expensive set is: {createdSets.Max()}");
            Console.WriteLine(string.Join(" ", createdSets));
        }
    }
}
