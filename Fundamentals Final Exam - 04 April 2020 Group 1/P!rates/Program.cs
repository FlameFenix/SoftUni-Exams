using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace P_rates
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = string.Empty;

            List<Cities> Cities = new List<Cities>();

            while ((command = Console.ReadLine()) != "Sail")
            {
                string[] cmdArgs = command.Split("||", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();

                string cityName = cmdArgs[0];
                int populations = int.Parse(cmdArgs[1]);
                int gold = int.Parse(cmdArgs[2]);

                Cities currentCity = new Cities(cityName, populations, gold);

                if (Cities.Any(x => x.CityName == currentCity.CityName))
                {
                    foreach (var item in Cities)
                    {
                        if (item.CityName == currentCity.CityName)
                        {
                            item.Gold += gold;
                            item.Populations += populations;
                        }
                    }
                }
                else
                {
                    Cities.Add(currentCity);
                }
            }

            command = Console.ReadLine();

            while (command != "End")
            {
                string[] cmdArgs = command.Split("=>", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();
                string option = cmdArgs[0];

                if (option == "Plunder")
                {
                    string town = cmdArgs[1];
                    int people = int.Parse(cmdArgs[2]);
                    int gold = int.Parse(cmdArgs[3]);

                    Console.WriteLine($"{town} plundered! {gold} gold stolen, {people} citizens killed.");

                    foreach (var item in Cities)
                    {
                        if (item.CityName == town)
                        {
                            if (item.Populations <= 0 || item.Gold <= 0)
                            {
                                continue;
                            }
                            else
                            {
                                item.Populations -= people;
                                item.Gold -= gold;
                                if (item.Populations <= 0 || item.Gold <= 0)
                                {
                                    Console.WriteLine($"{town} has been wiped off the map!");
                                }
                            }
                        }
                    }

                }
                else if (option == "Prosper")
                {
                    string cityName = cmdArgs[1];
                    int gold = int.Parse(cmdArgs[2]);
                    if (gold < 0)
                    {
                        Console.WriteLine("Gold added cannot be a negative number!");
                    }
                    else
                    {
                        foreach (var item in Cities)
                        {
                            if (item.CityName == cityName)
                            {
                                item.Gold += gold;
                                Console.WriteLine($"{gold} gold added to the city treasury. {cityName} now has {item.Gold} gold.");
                            }
                        }
                    }
                }

                command = Console.ReadLine();
            }

            int count = 0;

            foreach (var item in Cities)
            {
                if (item.Gold > 0 && item.Populations > 0)
                {
                    count++;
                }
            }
            Console.WriteLine($"Ahoy, Captain! There are {count} wealthy settlements to go to:");

            foreach (var item in Cities.OrderByDescending(x => x.Gold).ThenBy(x => x.CityName))
            {
                if (item.Gold > 0 && item.Populations > 0)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }

    public class Cities
    {
        public Cities(string cityName, int population, int gold)
        {
            CityName = cityName;
            Populations = population;
            Gold = gold;
        }
        public string CityName { get; set; }
        public int Populations { get; set; }
        public int Gold { get; set; }
        public override string ToString()
        {
            return $"{CityName} -> Population: {Populations} citizens, Gold: {Gold} kg";
        }


    }
}
