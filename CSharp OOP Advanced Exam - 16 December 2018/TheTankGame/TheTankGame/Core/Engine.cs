namespace TheTankGame.Core
{
    using System;
    using System.Linq;
    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(
            IReader reader, 
            IWriter writer, 
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

            this.isRunning = false;
        }

        public void Run()
        {
            string[] command = reader.ReadLine().Split();


            string cmdArgs = command[0];
            string message = string.Empty;

            while (true)
            {
                try
                {
                    message = commandInterpreter.ProcessInput(command.ToList());
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                
                writer.WriteLine(message);
                if(cmdArgs == "Terminate")
                {
                    break;
                }
                command = reader.ReadLine().Split();
                cmdArgs = command[0];
            }
        }
    }
}