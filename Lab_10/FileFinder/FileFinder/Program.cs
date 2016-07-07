using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder
{
    class Program
    {
        static void Main(string[] args)
        {

            FileFinder fileFinder = new FileFinder();

            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine("Enter <directory> <search term> or -e for exit");

                string input = Console.ReadLine();
                string[] commands;

                try
                {
                    commands = fileFinder.ParseAndCheckCommands(input);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Directory not found");
                    continue;
                }
                catch (InvalidCommandException)
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }

                if (commands[0] == "-e")
                {
                    isExit = true;
                    continue;
                }

                List<string> resulFiles = fileFinder.SearchForFiles(commands[0], commands[1]);

                if (resulFiles.Count == 0)
                {
                    Console.WriteLine("No files found.");
                }
                else
                {
                    foreach (var file in resulFiles)
                    {
                        Console.WriteLine(file);
                    }
                    Console.WriteLine($"{resulFiles.Count} files found.");
                }

            }
        }

        
    }
}
