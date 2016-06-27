using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleIO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What's your name?");
            string userName = Console.ReadLine();
            Console.WriteLine("Hello " + userName);

            int number = 0;
            bool parseSucceeded = false;

            while (!parseSucceeded || number < 1 || number > 10)
            {
                Console.WriteLine("Please enter whole number from 1 to 10");
                parseSucceeded = int.TryParse(Console.ReadLine(), out number);
            }

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine(userName);
                userName = " " + userName;
            }
        }
    }
}
