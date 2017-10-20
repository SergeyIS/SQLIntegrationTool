using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fclp;
using SQLIntegrationTool.Applications;

namespace SQLIntegrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandParser = new FluentCommandLineParser<CommandLineArguments>();
            CommandLineArguments.BuildParser(commandParser);
            Application application = null;
            var buf = String.Empty;

            Console.WriteLine("SQLIntegrationTool\nThis is simple tool for transfering data from one database to another database (IF STANDART WAYES DOESN'T WORK PROPERLY).\n\n\nWARRING: YOU NEED TO HAVE SAME DB STRUCTURE IN BOOTH DATABASES");
            while (buf != "exit")
            {
                Console.Write("> ");

                try
                {
                    buf = Console.ReadLine();
                    Console.WriteLine();

                    var splitted = buf.Split(' ');
                    if (String.IsNullOrEmpty(buf) || splitted == null || splitted.Count() == 0)
                        throw new Exception("Incorrect arguments");

                    //chose application
                    switch (splitted[0])
                    {
                        case "from":
                            application = new InitFromApplication();
                            break;
                        case "to":
                            application = new InitToApplication();
                            break;
                        case "trycopy":
                            application = new TryCopyApplication();
                            break;
                    };

                    if (application == null)
                        throw new Exception("Command doesn't exist");

                    commandParser.Parse(splitted);

                    Task commandRunner = application.Run(commandParser.Object);               
                    while (!commandRunner.IsCompleted)
                    {
                        //Console.SetCursorPosition(Console.CursorLeft + application.ProcessState.Length, Console.CursorTop - 1);
                        Console.SetCursorPosition(0, Console.CursorTop-1);
                        Console.WriteLine(application.ProcessState);
                        
                        System.Threading.Thread.Sleep(500);
                    }

                    application = null;

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
