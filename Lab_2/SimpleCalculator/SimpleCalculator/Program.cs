using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double firstNumber = 0;
            double secondNumber = 0;
            double result = 0;
            string oper = "";

            firstNumber = ParseDouble("Please enter first number");
            secondNumber = ParseDouble("Please enter second number");

            bool parseSucceeded = false;

            while (!parseSucceeded)
            {
                Console.WriteLine("Please enter operator (+, -, *, /)");
                oper = Console.ReadLine();
                parseSucceeded = oper == "+" || oper == "-" || oper == "*" || oper == "/";
                if (!parseSucceeded)
                {
                    Console.WriteLine("Operator is incorrect");
                }
            }

            switch (oper)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    result = firstNumber / secondNumber;
                    break;
            }

            Console.WriteLine(result.ToString());
        }

        static double ParseDouble(string message)
        {
            bool parseSucceeded = false;
            double result = 0;

            while (!parseSucceeded)
            {
                Console.WriteLine(message);
                parseSucceeded = double.TryParse(Console.ReadLine(), out result);
                if (!parseSucceeded)
                {
                    Console.WriteLine("Number is incorrect");
                }
            }
            return result;
        }
    }
}
