using System;
using System.Text.RegularExpressions;

namespace _02._Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfOperations = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfOperations; i++)
            {
                string message = Console.ReadLine();

                string pattern = @"^(\$|%)(?<tagName>[A-Z]{1}[a-z]{2,})\1: \[(?<one>[\d]+)\]\|\[(?<two>[\d]+)\]\|\[(?<three>[\d]+)\]\|$";

                Regex regex = new Regex(pattern);

                Match match = regex.Match(message);

                if(match.Success)
                {
                    string name = match.Groups["tagName"].Value;

                    int firstNumber = int.Parse(match.Groups["one"].Value);
                    int secondNumber = int.Parse(match.Groups["two"].Value);
                    int thirdNumber = int.Parse(match.Groups["three"].Value);

                    string key = string.Empty;

                    key += (char)firstNumber;
                    key += (char)secondNumber;
                    key += (char)thirdNumber;
                    Console.WriteLine($"{name}: {key}");
                }
                else
                {
                    Console.WriteLine("Valid message not found!");
                }
            }
        }
    }
}
