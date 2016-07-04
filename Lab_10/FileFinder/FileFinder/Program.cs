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
                if (input != null)
                {
                    var commands = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length == 1 && commands[0] == "-e")
                    {
                        isExit = true;
                    }
                    else if (commands.Length == 2)
                    {
                        
                    }
               }
            }
        }
    }
}
