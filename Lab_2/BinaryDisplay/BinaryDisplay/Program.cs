using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDisplay
{
    class Program
    {
        static void Main(string[] args)
        {

            int number = 0;
            bool parseSucceeded = false;

            while (!parseSucceeded)
            {
                Console.WriteLine("Please enter whole number");
                parseSucceeded = int.TryParse(Console.ReadLine(), out number);
            }

            string binary = "";
            while (number > 0)
            {
                binary = (number & 1) + binary;
                number = number >> 1;
            }

            Console.WriteLine(binary);
        }
    }
}
