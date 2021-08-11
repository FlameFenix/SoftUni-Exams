using PlayersAndMonsters.Core.Contracts;
using PlayersAndMonsters.IO;
using PlayersAndMonsters.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Core
{
    public class Engine : IEngine
    {
        private ManagerController controller;
        private IReader reader;
        private IWriter writer;
        public void Run()
        {
            controller = new ManagerController();
            reader = new Reader();
            writer = new Writer();
            string message = string.Empty;

            string input = reader.ReadLine();
            while (input != "Exit")
            {
                string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string command = cmdArgs[0];
                try
                {
                    switch (command)
                    {
                        case "AddPlayer":
                            // AddPlayer { player type} { player username}
                            string playerType = cmdArgs[1];
                            string playerName = cmdArgs[2];

                            message = controller.AddPlayer(playerType, playerName);
                         //   input = reader.ReadLine();
                            break;
                        case "AddCard":
                            //•	AddCard { card type} { card name}
                            string cardType = cmdArgs[1];
                            string cardName = cmdArgs[2];
                            message = controller.AddCard(cardType, cardName);

                         //   input = reader.ReadLine();
                            break;
                        case "AddPlayerCard":
                            string username = cmdArgs[1];
                            cardName = cmdArgs[2];
                            message = controller.AddPlayerCard(username, cardName);

                            
                            //•	AddPlayerCard { username} { card name}
                            break;
                        case "Fight":
                            string attacker = cmdArgs[1];
                            string victim = cmdArgs[2];
                         //   input = reader.ReadLine();

                            message = controller.Fight(attacker, victim);
                            //•	Fight { attack user} { enemy user}
                            break;
                        case "Report":
                            //•	Report
                           // input = reader.ReadLine();
                            message = controller.Report();
                            break;

                        default:
                            break;

                            
                    }
                }
                catch (ArgumentException ex)
                {
                    message = ex.Message;
                }
                
                writer.WriteLine(message);
                input = reader.ReadLine();
            }
        }
    }
}
