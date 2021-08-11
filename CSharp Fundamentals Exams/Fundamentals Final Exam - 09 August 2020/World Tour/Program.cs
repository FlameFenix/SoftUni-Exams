using System;
using System.Linq;

namespace World_Tour
{
    class Program
    {
        static void Main(string[] args)
        {
            string destination = Console.ReadLine();

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Travel")
            {
                string[] cmdArgs = command.Split(":", StringSplitOptions.RemoveEmptyEntries).ToArray();

                string option = cmdArgs[0];

                if(option == "Add Stop")
                {
                    int startIndex = int.Parse(cmdArgs[1]);
                    string substring = cmdArgs[2];

                    if(startIndex >= 0 && startIndex < destination.Length)
                    {
                        destination = destination.Insert(startIndex, substring);
                    }
                }
                else if(option == "Remove Stop")
                {
                    int startIndex = int.Parse(cmdArgs[1]);
                    int endIndex = int.Parse(cmdArgs[2]);

                    if(startIndex >= 0 && startIndex < destination.Length)
                    {
                        if(endIndex >= 0 && endIndex < destination.Length)
                        {
                            int count = endIndex - startIndex;
                            destination = destination.Remove(startIndex, count + 1);
                        }
                    }
                }
                else if(option == "Switch")
                {
                    string oldString = cmdArgs[1];
                    string newString = cmdArgs[2];

                    destination = destination.Replace(oldString, newString);
                }
                Console.WriteLine(destination);
            }
            Console.WriteLine($"Ready for world tour! Planned stops: {destination}");
        }
    }
}
