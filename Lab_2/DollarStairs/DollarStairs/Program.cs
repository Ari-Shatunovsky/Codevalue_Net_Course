using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarStairs
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 0;
            bool parseSucceeded = false;
            string dollars = "$";

            while (!parseSucceeded)
            {
                Console.WriteLine("Please enter whole number");
                parseSucceeded = int.TryParse(Console.ReadLine(), out number);
            }
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine(dollars);
                dollars += "$";
            }
        }
    }
}
