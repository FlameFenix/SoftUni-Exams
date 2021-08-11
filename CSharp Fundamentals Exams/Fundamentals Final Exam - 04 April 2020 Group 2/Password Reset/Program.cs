using System;
using System.Linq;

namespace Password_Reset
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = Console.ReadLine();

            string command = string.Empty;

            while((command = Console.ReadLine()) != "Done")
            {
                string[] cmdArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();

                string option = cmdArgs[0];

                if(option == "TakeOdd")
                {
                    string newPassword = string.Empty;

                    for (int i = 0; i < password.Length; i++)
                    {
                        if(i % 2 == 1)
                        {
                            newPassword += password[i];
                        }
                        
                    }

                    password = newPassword;
                }
                else if(option == "Cut")
                {
                    int startIndex = int.Parse(cmdArgs[1]);
                    int length = int.Parse(cmdArgs[2]);

                    if (password.Contains(password.Substring(startIndex, length)))
                    {
                        int indexOfFirstOcc = password.IndexOf(password.Substring(startIndex, length));
                        password = password.Remove(indexOfFirstOcc, length);
                    }
                }
                else if(option == "Substitute")
                {
                    string substring = cmdArgs[1];
                    string substitute = cmdArgs[2];

                    if(password.Contains(substring))
                    {
                        password = password.Replace(substring, substitute);
                    }
                    else
                    {
                        Console.WriteLine("Nothing to replace!");
                        continue;
                    }
                }

                Console.WriteLine(password);

            }

            Console.WriteLine($"Your password is: {password}");
        }
    }
}
