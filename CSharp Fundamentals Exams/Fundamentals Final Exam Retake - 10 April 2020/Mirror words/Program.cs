using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mirror_words
{
    class Program
    {
        static void Main(string[] args)
        {
            string words = Console.ReadLine();

            string pattern = @"(@|#{1})[A-Za-z]{3,}\1{2}[A-Za-z]{3,}\1";

            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(words);

            List<string> mirrorWords = new List<string>();

            foreach (Match item in matches)
            {
                if (item.Success)
                {
                    string match = item.Value;
                    match = match.Replace("#", " ");
                    match = match.Replace("@", " ");

                    string[] couple = match.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                           .ToArray();

                    string firstWord = couple[0];
                    string secondWord = couple[1];

                    char[] secondWordToChar = secondWord.ToCharArray();

                    secondWordToChar = secondWordToChar.Reverse().ToArray();

                    string reversedWord = new string(secondWordToChar);

                    if (firstWord == reversedWord)
                    {
                        string mirrorWord = $"{couple[0]} <=> {couple[1]}";
                        mirrorWords.Add(mirrorWord);
                    }
                }

            }

            if (matches.Count == 0)
            {
                Console.WriteLine("No word pairs found!");
            }
            else
            {
                Console.WriteLine($"{matches.Count} word pairs found!");
            }

            if (mirrorWords.Count == 0)
            {
                Console.WriteLine("No mirror words!");
            }
            else
            {
                Console.WriteLine("The mirror words are: ");
                Console.WriteLine(string.Join(", ", mirrorWords));
            }

        }
    }
}
