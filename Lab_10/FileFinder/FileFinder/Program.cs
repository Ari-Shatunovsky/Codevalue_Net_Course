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

            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine("Enter <directory> <search term> or -e for exit");

                string input = Console.ReadLine();
                string[] commands;

                try
                {
                    commands = ParseAndCheckCommands(input);
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

                List<string> resulFiles = SearchForFiles(commands[0], commands[1]);

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

        static List<string> SearchForFiles(string directory, string searchTerm)
        {
            List<string> resultFiles = new List<string>();
            string[] subDirectories = Directory.GetDirectories(directory);
            string[] files = Directory.GetFiles(directory);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                if (fileName.ToLowerInvariant().Contains(searchTerm))
                {
                    resultFiles.Add(file);
                }
            }

            foreach (var subDirectory in subDirectories)
            {
                Console.WriteLine($"Searching in {subDirectory}");
                resultFiles.AddRange(SearchForFiles(subDirectory, searchTerm));
            }

            return resultFiles;
        }

        static string[] ParseAndCheckCommands(string text)
        {
            if (text != null)
            {
                var commands = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if (commands.Length == 1 && commands[0] == "-e" || commands.Length == 2 && Directory.Exists(commands[0]))
                {
                    return commands;
                }
                if (!Directory.Exists(commands[0]))
                {
                    throw new DirectoryNotFoundException();
                }
            }
            throw new InvalidCommandException();
        }
    }
}
