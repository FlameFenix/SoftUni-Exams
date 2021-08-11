using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Destination_Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string destinations = Console.ReadLine();

            string pattern = @"(={1}|\/{1})[A-Z][A-Za-z]{2,}\1";

            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(destinations);

            int travelPoints = 0;
            List<string> output = new List<string>();

            foreach (Match item in matches)
            {
                string currentDestination = item.Value;
                currentDestination = currentDestination.Replace("=", "");
                currentDestination = currentDestination.Replace(@"/", "");
                travelPoints += currentDestination.Length;
                output.Add(currentDestination);
            }

            Console.WriteLine($"Destinations: {string.Join(", ", output)}");
            Console.WriteLine($"Travel Points: {travelPoints}");
        }
    }
}
