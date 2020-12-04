using System;

namespace The_Imitation_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = Console.ReadLine();

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Decode")
            {
                string[] cmdArgs = command.Split("|", StringSplitOptions.RemoveEmptyEntries);

                string option = cmdArgs[0];

                if(option == "Move")
                {
                    int numberOfLetters = int.Parse(cmdArgs[1]);

                    string currentLetters = string.Empty;

                    for (int i = 0; i < numberOfLetters; i++)
                    {

                        currentLetters += message[i];
                    }

                    message = message.Remove(0, numberOfLetters);
                    message = message.Insert(message.Length, currentLetters);

                }
                else if(option == "Insert")
                {
                    int index = int.Parse(cmdArgs[1]);

                    string value = cmdArgs[2];

                    message = message.Insert(index, value);
                }
                else if(option == "ChangeAll")
                {
                    string substring = cmdArgs[1];
                    string replacement = cmdArgs[2];

                    message = message.Replace(substring, replacement);
                }
            }
            Console.WriteLine($"The decrypted message is: {message}");
        }
    }
}
