using System;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox
{
    class Program
    {
        static void Main(string[] args)
        {

            Queue<int> firstBox = new Queue<int>
                (Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray());

            Stack<int> secondBox = new Stack<int>
                (Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray());

            int claimedItems = 0;

            while (firstBox.Count > 0 && secondBox.Count > 0)
            {
                int firsBoxValue = firstBox.Peek();
                int secondBoxValue = secondBox.Peek();

                if((firsBoxValue + secondBoxValue) % 2 == 0)
                {
                    claimedItems += (firsBoxValue + secondBoxValue);
                    firstBox.Dequeue();
                    secondBox.Pop();
                }
                else
                {
                    secondBox.Pop();
                    firstBox.Enqueue(secondBoxValue);
                }
            }
            if(firstBox.Count == 0)
            {
                Console.WriteLine("First lootbox is empty");
            }
            else if(secondBox.Count == 0)
            {
                Console.WriteLine("Second lootbox is empty");
            }
            
            if(claimedItems >= 100)
            {
                Console.WriteLine($"Your loot was epic! Value: {claimedItems}");
            }
            else
            {
                Console.WriteLine($"Your loot was poor... Value: {claimedItems}");
            }
        }
    }
}
