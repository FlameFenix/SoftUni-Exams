using System;
using System.Linq;

namespace Secret_Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = Console.ReadLine();
            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Reveal")
            {
                string[] cmdArgs = command.Split(":|:", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string option = cmdArgs[0];

                if (option == "InsertSpace")
                {
                    int startIndex = int.Parse(cmdArgs[1]);
                    if(startIndex >= 0 && startIndex <= message.Length)
                    {
                        message = message.Insert(startIndex, " ");
                    }                
                }
                else if (option == "Reverse")
                {
                    string substring = cmdArgs[1];

                    string substringReversed = string.Empty;

                    if (message.Contains(substring))
                    {
                        int startIndex = message.IndexOf(substring);
                        

                        for (int i = substring.Length - 1; i >= 0; i--)
                        {
                            substringReversed += substring[i];
                        }

                        message = message.Remove(startIndex, substring.Length);
                        message = message.Insert(message.Length, substringReversed);
                    }
                    else
                    {
                        Console.WriteLine("error");
                        continue;
                    }
                }
                else if (option == "ChangeAll")
                {
                    string substring = cmdArgs[1];
                    string replacement = cmdArgs[2];

                    if(message.Contains(substring))
                    {
                        message = message.Replace(substring, replacement);
                    }
                }
                Console.WriteLine(message);
            }

            Console.WriteLine($"You have a new text message: {message}");
        }
    }
}
