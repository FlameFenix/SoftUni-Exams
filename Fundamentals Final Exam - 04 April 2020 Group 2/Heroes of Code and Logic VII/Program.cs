using System;
using System.Collections.Generic;
using System.Linq;

namespace Heroes_of_Code_and_Logic_VII
{
    class Program
    {
        static void Main(string[] args)
        {
            int numbersOfPlayers = int.Parse(Console.ReadLine());

            List<Heroes> heroes = new List<Heroes>();

            for (int i = 0; i < numbersOfPlayers; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string name = input[0];
                int heal = int.Parse(input[1]);
                int mana = int.Parse(input[2]);

                if (heal > 100)
                {
                    heal = 100;
                }
                if (mana > 200)
                {
                    mana = 200;
                }

                Heroes hero = new Heroes(name, heal, mana);

                heroes.Add(hero);

            }

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = command.Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();
                string option = cmdArgs[0];

                if (option == "CastSpell")
                {
                    string heroName = cmdArgs[1];
                    int manaNeeded = int.Parse(cmdArgs[2]);
                    string spellName = cmdArgs[3];

                    foreach (var item in heroes)
                    {
                        if (item.Name == heroName && item.Heal > 0)
                        {
                            if (item.Mana >= manaNeeded)
                            {
                                item.Mana -= manaNeeded;
                                Console.WriteLine($"{heroName} has successfully cast {spellName} and now has {item.Mana} MP!");
                            }
                            else
                            {
                                Console.WriteLine($"{heroName} does not have enough MP to cast {spellName}!");
                            }
                        }
                    }
                }
                else if (option == "TakeDamage")
                {
                    string heroName = cmdArgs[1];
                    int damage = int.Parse(cmdArgs[2]);
                    string attacker = cmdArgs[3];

                    foreach (var item in heroes)
                    {
                        if(item.Name == heroName && item.Heal > 0)
                        {
                                if (item.Heal > damage)
                                {
                                    item.Heal -= damage;
                                    Console.WriteLine($"{heroName} was hit for {damage} HP by {attacker} and now has {item.Heal} HP left!");
                                }
                                else
                                {
                                    item.Heal -= damage;
                                    Console.WriteLine($"{heroName} has been killed by {attacker}!");
                                }
                        }
                    }
                }
                else if (option == "Recharge")
                {
                    string heroName = cmdArgs[1];
                    int amount = int.Parse(cmdArgs[2]);

                    foreach (var item in heroes)
                    {
                        if(item.Name == heroName && item.Heal > 0)
                        {
                            int amountOfRecovery = 0;
                            if(item.Mana + amount <= 200)
                            {
                                item.Mana += amount;
                                amountOfRecovery = amount;
                            }
                            else
                            {
                                amountOfRecovery = 200 - item.Mana;
                                item.Mana = 200;
                            }
                            Console.WriteLine($"{heroName} recharged for {amountOfRecovery} MP!");
                        }
                    }
                }
                else if (option == "Heal")
                {
                    string heroName = cmdArgs[1];
                    int amount = int.Parse(cmdArgs[2]);

                    foreach (var item in heroes)
                    {
                        if(item.Name == heroName && item.Heal > 0)
                        {
                            int amountOfRecovery = 0;
                            if(item.Heal + amount <= 100)
                            {
                                item.Heal += amount;
                                amountOfRecovery = amount;
                            }
                            else
                            {
                                amountOfRecovery = 100 - item.Heal;
                                item.Heal = 100;
                            }
                            Console.WriteLine($"{heroName} healed for {amountOfRecovery} HP!");
                        }
                    }
                }
            }

            foreach (var item in heroes.OrderByDescending(x => x.Heal).ThenBy(x => x.Name))
            {
                if(item.Heal > 0)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine($"HP: {item.Heal}");
                    Console.WriteLine($"MP: {item.Mana}");
                }
            }
        }
    }

    class Heroes
    {
        public Heroes(string name, int heal, int mana)
        {
            Name = name;
            Heal = heal;
            Mana = mana;
        }
        public string Name { get; set; }
        public int Heal { get; set; }
        public int Mana { get; set; }

    }
}

