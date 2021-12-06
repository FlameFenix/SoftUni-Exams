using System;
using System.Collections.Generic;
using System.Linq;

namespace Easter_Bonuses
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = string.Empty;

            int sumOfBonuses = 0;

            while ((command = Console.ReadLine()) != "stop")
            {
                string name = command;
                int[] targets = Console.ReadLine()
                                       .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToArray();

                string currentSum = string.Empty;
                List<int> sums = new List<int>();

                for (int i = 0; i < targets.Length; i++)
                {
                    int sum = 1;

                    for (int j = 0; j < targets.Length; j++)
                    {
                        if(j != i)
                        {
                            sum *= targets[j];
                        }
                    }
                    sums.Add(sum);
                }

                int finalSum = 0;

                foreach (var item in sums)
                {
                    finalSum += item;
                }
                Console.WriteLine($"{name} has a bonus of {finalSum} lv.");
                sumOfBonuses += finalSum;
            }

            Console.WriteLine($"The amount of all bonuses is {sumOfBonuses} lv.");
        }
    }
}
