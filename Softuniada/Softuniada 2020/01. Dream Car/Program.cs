using System;
using System.Globalization;

namespace _01._Dream_Car
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal n = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture); // Starting salary
            decimal m = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture); // Monthly costs
            decimal x = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture); // Salary increases monthly
            decimal y = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture); // Price per car
            int t = int.Parse(Console.ReadLine()); // months

            decimal increasedSalary = 0;

            decimal costs = m * t;

            for (int i = 0; i < t; i++)
            {
                increasedSalary += n;
                n += x;
            }

            decimal result = increasedSalary - costs;

            string output = result >= y ? "Have a nice ride!" : "Work harder!";

            Console.WriteLine(output);

        }
    }
}
