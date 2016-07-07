using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder
{
    public class FileFinder
    {
        public List<string> SearchForFiles(string directory, string searchTerm)
        {
            List<string> resultFiles = new List<string>();
            try
            {
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
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Cannot access to {directory}, skip");    
            }

            return resultFiles;
        }

        public string[] ParseAndCheckCommands(string text)
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
