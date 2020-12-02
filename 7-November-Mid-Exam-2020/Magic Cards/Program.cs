using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;

namespace Magic_Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> cards = Console.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries).ToList();

            string cmdArgs = string.Empty;

            List<string> newDeck = new List<string>();

            
            while((cmdArgs = Console.ReadLine()) != "Ready")
            {
                string[] commands = cmdArgs.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                           .ToArray();

                string currentCommand = commands[0];

                if(currentCommand == "Add")
                {
                    string currentCard = commands[1];

                    if(cards.Contains(currentCard))
                    {
                        newDeck.Add(currentCard);
                    }
                    else
                    {
                        Console.WriteLine("Card not found.");
                    }
                }
                else if(currentCommand == "Insert")
                {
                    string currentCard = commands[1];
                    int currentIndex = int.Parse(commands[2]);

                    if(( currentIndex >= 0 && currentIndex < newDeck.Count ) && cards.Contains(currentCard))
                    {
                        newDeck.Insert(currentIndex, currentCard);
                    }
                    else
                    {
                        Console.WriteLine("Error!");
                    }
                }
                else if(currentCommand == "Remove")
                {
                    string currentCard = commands[1];
                    if(newDeck.Contains(currentCard))
                    {
                        newDeck.Remove(currentCard);
                    }
                    else
                    {
                        Console.WriteLine("Card not found.");
                    }
                }

                else if(currentCommand == "Swap")
                {
                    string firstCard = commands[1];
                    string secondCard = commands[2];

                    int indexOfFirstCard = newDeck.IndexOf(firstCard);
                    int indexOfSecondCard = newDeck.IndexOf(secondCard);

                    newDeck.RemoveAt(indexOfFirstCard);
                    newDeck.Insert(indexOfFirstCard, secondCard);

                    newDeck.RemoveAt(indexOfSecondCard);
                    newDeck.Insert(indexOfSecondCard, firstCard);
                }

                else if(cmdArgs == "Shuffle deck")
                {
                    newDeck.Reverse();
                }
            }
            Console.WriteLine(string.Join(" ", newDeck));
        }
    }
}
