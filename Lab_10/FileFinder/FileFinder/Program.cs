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
                string[] commands;
                Console.WriteLine("Enter <directory> <search term> or -e for exit");
                commands = Console.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if (commands.Length == 1 && commands[0] == "-e")
                {
                    isExit = true;
                } else if (commands.Length == 2)
                {
                    string directoryName = commands[0];
                    string searchTerm = commands[1];
                    if (Directory.Exists(directoryName))
                    {
                        string[] fileNames = Directory.GetFiles(directoryName);
                        foreach (var fileName in fileNames)
                        {
                            if (File.ReadAllText(fileName).IndexOf(searchTerm, StringComparison.Ordinal) >= 0)
                            {
                                Console.WriteLine("Text {0} found in file {1}.", searchTerm, fileName);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Directiry don't exists");
                    }
                }
                else
                {
                    Console.WriteLine("Command is not supported.");
                }
            }
        }
    }
}
