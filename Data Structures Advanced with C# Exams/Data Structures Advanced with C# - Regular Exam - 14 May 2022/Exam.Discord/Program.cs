using System;
using System.Collections.Generic;

namespace Exam.Discord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine( "test");
            Discord discord = new Discord();

            discord.SendMessage(new Message("4353", "hello", 60, "muorigin3"));

            discord.ReactToMessage("4353", "like");
            discord.ReactToMessage("4353", "like2");
            discord.ReactToMessage("4353", "like3");

            var reactions = new List<string>()
            {
                "like",
                "like2",
                "like3"
            };

            var result = discord.GetMessagesByReactions(reactions);

            Console.WriteLine(string.Join(' ', result));

            var message = discord.GetMessage("4353");

            Console.WriteLine(discord.Contains(message));
        }
    }
}
