using System;
using System.Linq;

namespace Activation_Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            string activationKey = Console.ReadLine();

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Generate")
            {
                string[] cmdArgs = command.Split(">>>", StringSplitOptions.RemoveEmptyEntries).ToArray();

                string option = cmdArgs[0];

                int count = 0;

                if (option == "Contains")
                {
                    string substring = cmdArgs[1];
                    if (activationKey.Contains(substring))
                    {
                        Console.WriteLine($"{activationKey} contains {substring}");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Substring not found!");
                        continue;
                    }
                }
                else if (option == "Flip")
                {
                    string secondOption = cmdArgs[1];
                    int startIndex = int.Parse(cmdArgs[2]);
                    int endIndex = int.Parse(cmdArgs[3]);
                    if (startIndex >= 0 && endIndex < activationKey.Length)
                    {
                        if (secondOption == "Upper")
                        {
                            string currentWord = string.Empty;
                            string newWord = string.Empty;
                            for (int i = startIndex; i < endIndex; i++)
                            {

                                string currentChar = activationKey[i].ToString().ToUpper();
                                currentWord += activationKey[i];
                                newWord += char.Parse(currentChar);
                            }

                            if (activationKey.Contains(currentWord))
                            {
                                count = endIndex - (startIndex + 1);
                                activationKey = activationKey.Remove(startIndex, count + 1);
                                activationKey = activationKey.Insert(startIndex, newWord);
                            }
                        }
                        else if (secondOption == "Lower")
                        {
                            string currentWord = string.Empty;
                            string newWord = string.Empty;
                            for (int i = startIndex; i < endIndex; i++)
                            {

                                string currentChar = activationKey[i].ToString().ToLower();
                                currentWord += activationKey[i];
                                newWord += char.Parse(currentChar);
                            }

                            if (activationKey.Contains(currentWord))
                            {
                                count = endIndex - (startIndex + 1);
                                activationKey = activationKey.Remove(startIndex, count + 1);
                                activationKey = activationKey.Insert(startIndex, newWord);
                            }
                        }

                    }

                }
                else if (option == "Slice")
                {
                    int startIndex = int.Parse(cmdArgs[1]);
                    int endIndex = int.Parse(cmdArgs[2]);

                    if (startIndex >= 0 && endIndex < activationKey.Length)
                    {
                        count = endIndex - startIndex;
                        activationKey = activationKey.Remove(startIndex, count);
                    }
                }
                Console.WriteLine(activationKey);
            }
            Console.WriteLine($"Your activation key is: {activationKey}");
        }
    }
}
