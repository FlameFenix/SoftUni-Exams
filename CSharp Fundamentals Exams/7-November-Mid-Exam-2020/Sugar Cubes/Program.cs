using System;
using System.Collections.Generic;
using System.Linq;

namespace Sugar_Cubes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sugarCubes = Console.ReadLine()
                                      .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                      .Select(int.Parse)
                                      .ToList();

            string cmdArgs = string.Empty;

            while((cmdArgs = Console.ReadLine()) != "Mort")
            {
                string[] commands = cmdArgs.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                 .ToArray();

                string currentCommand = commands[0];

                if(currentCommand == "Add")
                {
                    int value = int.Parse(commands[1]);

                    sugarCubes.Add(value);
                }
                else if(currentCommand == "Remove")
                {
                    int value = int.Parse(commands[1]);

                    int indexOfFirst = 0;

                    for (int i = 0; i < sugarCubes.Count; i++)
                    {
                        if(value == sugarCubes[i])
                        {
                            indexOfFirst = i;
                            break;
                        }
                    }

                    sugarCubes.RemoveAt(indexOfFirst);
                }
                else if(currentCommand == "Replace")
                {
                    int value = int.Parse(commands[1]);

                    int replacement = int.Parse(commands[2]);

                    int indexOfValue = 0;

                    for (int i = 0; i < sugarCubes.Count; i++)
                    {
                        if (value == sugarCubes[i])
                        {
                            indexOfValue = i;
                            break;
                        }
                    }

                    sugarCubes.RemoveAt(indexOfValue);
                    sugarCubes.Insert(indexOfValue, replacement);

                }
                else if(currentCommand == "Collapse")
                {
                    int value = int.Parse(commands[1]);

                    for (int i = 0; i < sugarCubes.Count; i++)
                    {
                        if(value > sugarCubes[i])
                        {
                            sugarCubes.Remove(sugarCubes[i]);
                            i = -1;
                        }
                    }
                }
            }
            Console.WriteLine(string.Join(" ", sugarCubes));
        }
    }
}
