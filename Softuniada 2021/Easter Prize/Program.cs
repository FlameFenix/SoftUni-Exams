using System;
using System.Collections.Generic;

namespace Easter_Prize
{
    class Program
    {
        static void Main(string[] args)
        {
            int startNum = int.Parse(Console.ReadLine());
            int endNum = int.Parse(Console.ReadLine());
            List<int> nums = new List<int>();
            for (int i = startNum; i <= endNum; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < Math.Sqrt(endNum); j++)
                {
                    if(i % j == 0 && i != j && i > 2)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if(isPrime && i > 1)
                {
                    nums.Add(i);
                }
            }

            Console.WriteLine(string.Join(" ", nums));
            Console.WriteLine($"The total number of prime numbers between {startNum} to {endNum} is {nums.Count}");
        }
    }
}
