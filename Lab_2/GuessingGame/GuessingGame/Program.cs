using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int secret = new Random().Next(1, 100);

            Console.WriteLine("Try to guess number from 1 to 100");

            for (int i = 1; i <= 7; i++)
            {
                int userInput = 0;
                bool parseSucceed = int.TryParse(Console.ReadLine(), out userInput);

                if (!parseSucceed)
                {
                    Console.WriteLine("Enter whole number");
                }
                else if (userInput < secret)
                {
                    Console.WriteLine("Too small");
                }
                else if (userInput > secret)
                {
                    Console.WriteLine("Too big");
                }
                else
                {
                    Console.WriteLine("Congrats!! Number is {0}. You answered in {1} turns", secret, i);
                }
            }

            Console.WriteLine("You failed");
        }
    }
}
