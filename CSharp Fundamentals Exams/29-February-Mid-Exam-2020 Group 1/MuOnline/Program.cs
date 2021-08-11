using System;
using System.Collections.Generic;
using System.Linq;

namespace MuOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> rooms = Console.ReadLine().Split("|").ToList();

            List<string> details = new List<string>(rooms.Count);

            List<string> action = new List<string>(rooms.Count);

            List<int> value = new List<int>(rooms.Count);
            int chestSum = 0;
            bool isKilled = false;
            for (int i = 0; i < rooms.Count; i++)
            {
                details = rooms[i].Split(" ").ToList();
                action.Add(details[0]);
                value.Add(int.Parse(details[1]));
            }

            int healthOfPlayer = 100;
            int count = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                count++;
                int healthHealed = 0;
                if (action[i] == "potion")
                {
                    if (healthOfPlayer > 0 && healthOfPlayer < 100)
                    {

                        healthOfPlayer += value[i];
                        if (healthOfPlayer > 100)
                        {
                            healthHealed = value[i] - (healthOfPlayer - 100);
                            healthOfPlayer = 100;
                        }
                        else
                        {
                            healthHealed = value[i];
                        }
                    }
                    Console.WriteLine($"You healed for {healthHealed} hp.");
                    Console.WriteLine($"Current health: {healthOfPlayer} hp.");
                }
                else if (action[i] == "chest")
                {
                    chestSum += value[i];
                    Console.WriteLine($"You found {value[i]} bitcoins.");
                }
                else
                {
                    healthOfPlayer -= value[i];
                    if (healthOfPlayer > 0)
                    {
                        Console.WriteLine($"You slayed {action[i]}.");

                    }
                    else
                    {
                        Console.WriteLine($"You died! Killed by {action[i]}.");
                        Console.WriteLine($"Best room: {count}");
                        isKilled = true;
                        break;
                    }
                }
            }
            if (!isKilled)
            {
                Console.WriteLine($"You've made it!");
                Console.WriteLine($"Bitcoins: {chestSum}");
                Console.WriteLine($"Health: {healthOfPlayer}");
            }
        }
    }
}
