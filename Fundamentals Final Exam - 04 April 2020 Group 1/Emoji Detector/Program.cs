using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Emoji_Detector
{
    class Program
    {
        static void Main(string[] args)
        {
            string emojis = Console.ReadLine();

            string emojiPattern = @"(\*{2}|:{2})[A-Z][a-z]{2,}\1";

            string numbersPattern = @"\d";

            Regex emojiRegex = new Regex(emojiPattern);

            Regex numbersRegex = new Regex(numbersPattern);

            MatchCollection emoji = emojiRegex.Matches(emojis);

            MatchCollection numbers = numbersRegex.Matches(emojis);

            long value = 1;

            List<string> listEmoji = new List<string>();

            List<string> listEmojiWithOSplit = new List<string>();

            List<int> listNUmbers = new List<int>();

            foreach (Match item in emoji)
            {
                string currentValue = item.Value;
                listEmojiWithOSplit.Add(currentValue);

                currentValue = currentValue.Replace(":", "");
                currentValue = currentValue.Replace("*", "");
                listEmoji.Add(currentValue);

            }

            foreach (Match item in numbers)
            {
                listNUmbers.Add(int.Parse(item.Value));
            }

            foreach (var item in listNUmbers)
            {
                value *= item;
            }

            Console.WriteLine($"Cool threshold: {value}");


            Console.WriteLine($"{listEmoji.Count} emojis found in the text. The cool ones are:");
            int counter = 0;

            foreach (var item in listEmoji)
            {
                long currentValue = 1;

                foreach (var sum in item)
                {
                    int currentNumber = (int)sum;
                    currentValue += currentNumber;
                }

                if (currentValue > value)
                {
                    Console.WriteLine(listEmojiWithOSplit[counter]);
                }
                counter++;
            }
        }
    }
}
