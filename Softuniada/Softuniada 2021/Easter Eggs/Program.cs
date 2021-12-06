using System;

namespace Easter_Eggs
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            while (n >= 1)
            {
                for (int i = 1; i <= n; i++)
                {
                    Console.Write(i);
                }

                for (int i = n - 1; i >= 1; i--)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
                n--;
            }
        }
    }
}
