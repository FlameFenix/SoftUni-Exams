using System;

namespace _01._Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();

            string command = Console.ReadLine();

            while(command != "Sign up")
            {
                string[] cmdArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string option = cmdArgs[0];

                if(option == "Case")
                {
                    string secondOption = cmdArgs[1];
                    if(secondOption == "lower")
                    {
                        name = name.ToLower();
                    }
                    else
                    {
                        name = name.ToUpper();
                    }
                    Console.WriteLine(name);
                }
                else if(option == "Reverse")
                {
                    string reversedSubstring = string.Empty;
                    string substring = string.Empty;

                    int startIndex = int.Parse(cmdArgs[1]);
                    int endIndex = int.Parse(cmdArgs[2]);
                    if(startIndex >= 0 && endIndex < name.Length)
                    {
                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            substring += name[i];
                        }

                        for (int i = substring.Length - 1; i >= 0; i--)
                        {
                            reversedSubstring += substring[i];
                        }

                        Console.WriteLine(reversedSubstring);
                    } 
                }
                else if(option == "Cut")
                {
                    string substring = cmdArgs[1];
                    if (name.Contains(substring))
                    {
                        name = name.Replace(substring, "");
                        Console.WriteLine(name);
                    }
                    else
                    {
                        Console.WriteLine($"The word {name} doesn't contain {substring}.");
                    }
                }
                else if( option == "Replace")
                {
                    char currentChar = char.Parse(cmdArgs[1]);

                    if(name.Contains(currentChar))
                    {
                        name = name.Replace(currentChar, '*');
                        Console.WriteLine(name);
                    }
                }
                else if(option == "Check")
                {
                    char currentChar = char.Parse(cmdArgs[1]);

                    if(name.Contains(currentChar))
                    {
                        Console.WriteLine("Valid");
                    }
                    else
                    {
                        Console.WriteLine($"Your username must contain {currentChar}.");
                    }
                }

                command = Console.ReadLine();
            }
        }
    }
}
