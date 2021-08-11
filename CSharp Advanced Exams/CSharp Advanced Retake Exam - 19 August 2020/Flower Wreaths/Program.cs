using System;
using System.Collections.Generic;
using System.Linq;

namespace Flower_Wreaths
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] lilies = Console.ReadLine()
                                  .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                                  .Select(int.Parse)
                                  .ToArray();

            int[] roses = Console.ReadLine()
                                  .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                                  .Select(int.Parse)
                                  .ToArray();

            Stack<int> stackLilies = new Stack<int>(lilies);

            Queue<int> queueRoses = new Queue<int>(roses);

            int wreaths = 0;

            int sumLaterFlowers = 0;

            while (stackLilies.Count > 0 && queueRoses.Count > 0)
            {
                int currentLilies = stackLilies.Peek();
                int currentRoses = queueRoses.Peek();

                if(currentLilies + currentRoses == 15)
                {
                    wreaths++;
                    stackLilies.Pop();
                    queueRoses.Dequeue();
                }
                else if(currentLilies + currentRoses > 15)
                {
                    int LiliesToDecrease = stackLilies.Pop() - 2;
                    stackLilies.Push(LiliesToDecrease);
                }
                else if(currentLilies + currentRoses < 15)
                {
                    sumLaterFlowers += currentLilies + currentRoses;
                    if(sumLaterFlowers >= 15)
                    {
                        wreaths++;
                        sumLaterFlowers -= 15;
                    }
                    stackLilies.Pop();
                    queueRoses.Dequeue();
                }
            }

            if(wreaths >= 5)
            {
                Console.WriteLine($"You made it, you are going to the competition with {wreaths} wreaths!");
            }
            else
            {
                Console.WriteLine($"You didn't make it, you need {5 - wreaths} wreaths more!");
            }
        }
    }
}
