using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> items = Console.ReadLine().Split(", ").ToList();

            List<string> command = Console.ReadLine().Split(" - ").ToList();

            List<string> separetedItem = new List<string>();

            while (command[0] != "Craft!")
            {
                if (command[0] == "Collect")
                {
                    if (!items.Contains(command[1]))
                    {
                        items.Add(command[1]);
                    }
                }
                else if (command[0] == "Drop")
                {
                    if (items.Contains(command[1]))
                    {
                        items.Remove(command[1]);
                    }
                }
                else if (command[0] == "Combine Items")
                {
                    for (int i = 0; i < command.Count; i++)
                    {
                        separetedItem = command[1].Split(":").ToList();
                    }
                    if (items.Contains(separetedItem[0]))
                    {

                        int currentIndex = items.IndexOf(separetedItem[0]);
                        items.Insert(currentIndex + 1, separetedItem[1]);
                    }
                }
                else if (command[0] == "Renew")
                {
                    if (items.Contains(command[1]))
                    {
                        int currentIndex = items.IndexOf(command[1]);
                        items.Add(command[1]);
                        items.RemoveAt(currentIndex);
                    }
                }
                command = Console.ReadLine().Split(" - ").ToList();
            }
            Console.WriteLine(string.Join(", ", items));
        }
    }
}
