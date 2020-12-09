using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Fancy_Barcodes
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfCodes = int.Parse(Console.ReadLine());

            Dictionary<Match, string> matches = new Dictionary<Match, string>();

            for (int i = 0; i < numberOfCodes; i++)
            {
                string barcode = Console.ReadLine();

                string barcodePattern = @"(@{1}#+)[A-Z][A-Za-z0-9]{4,}[A-Z](@{1}#+)";

                string numbersPattern = @"\d+";

                Regex regex = new Regex(barcodePattern);

                Match match = regex.Match(barcode);

                if (match.Success)
                {
                    Regex numbers = new Regex(numbersPattern);

                    MatchCollection contNumbers = numbers.Matches(barcode);

                    bool isMatchNumbers = false;

                    string group = string.Empty;

                    if (contNumbers.Count > 0)
                    {
                        foreach (var item in contNumbers)
                        {
                            group += item;
                        }
                        isMatchNumbers = true;
                    }
                    else
                    {

                    }

                    if (isMatchNumbers)
                    {
                        Console.WriteLine($"Product group: {group}");
                    }
                    else
                    {
                        group = "00";
                        Console.WriteLine($"Product group: {group}");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid barcode");
                }
            }
        }
    }
}
