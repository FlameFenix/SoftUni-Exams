using System;
using System.Collections.Generic;
using System.Linq;

namespace Plant_Discovery
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPlants = int.Parse(Console.ReadLine());

            List<Plants> plants = new List<Plants>();

            for (int i = 0; i < numberOfPlants; i++)
            {
                string[] plant = Console.ReadLine().Split("<->", StringSplitOptions.RemoveEmptyEntries)
                                                   .ToArray();
                string plantName = plant[0];
                int rarity = int.Parse(plant[1]);

                Plants currentPlant = new Plants();

                if (plants.Contains(currentPlant))
                {
                    
                    plants.Remove(currentPlant);

                    currentPlant.PlantName = plantName;
                    currentPlant.Rarity = rarity;
                    currentPlant.counter = 0;
                    currentPlant.Rating = 0;

                    plants.Add(currentPlant);
                }
                else
                {
                    currentPlant.PlantName = plantName;
                    currentPlant.Rarity = rarity;
                    currentPlant.counter = 0;
                    currentPlant.Rating = 0;
                    plants.Add(currentPlant);
                }
                
            }

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Exhibition")
            {
                bool isExist = false;

                command = command.Replace("-", ":");
                command = command.Replace(" ", "");

                string[] cmdArgs = command.Split(":", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();
                string option = cmdArgs[0];

                if(option == "Rate")
                {
                    string plant = cmdArgs[1];
                    int rating = int.Parse(cmdArgs[2]);
                    foreach (var item in plants)
                    {
                        if(item.PlantName == plant)
                        {
                            item.Rating += rating;
                            item.counter += 1;
                            isExist = true;
                        }
                    }
                }
                else if(option == "Update")
                {
                    string plantName = cmdArgs[1];
                    int newRarity = int.Parse(cmdArgs[2]);
                    foreach (var item in plants)
                    {
                        if(item.PlantName == plantName)
                        {
                            item.Rarity = newRarity;
                            isExist = true;
                        }
                    }
                }
                else if(option == "Reset")
                {
                    string plantName = cmdArgs[1];
                    foreach (var item in plants)
                    {
                        if(item.PlantName == plantName)
                        {
                            item.Rating = 0;
                            item.counter = 0;
                            isExist = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("error");
                }
                if(!isExist)
                {
                    Console.WriteLine("error");
                }
            }
            Console.WriteLine("Plants for the exhibition:");
            foreach (var item in plants.OrderByDescending(x => x.Rarity).ThenByDescending(x => x.Rating))
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    class Plants
    {
        public string PlantName { get; set; }
        public int Rarity { get; set; }
        public double Rating { get; set; }

        public int counter { get; set; }

        public override string ToString()
        {
            if(Rating > 0)
            {
                return $"- {PlantName}; Rarity: {Rarity}; Rating: {Rating / counter:f2}";
            }
            else
            {
                return $"- {PlantName}; Rarity: {Rarity}; Rating: {0:f2}";
            }
        }
    }
}
