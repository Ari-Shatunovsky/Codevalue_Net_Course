using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0;
            double b = 0;
            double c = 0;

            if (args != null && args.Length == 3
                && double.TryParse(args[0], out a)
                && double.TryParse(args[1], out b)
                && double.TryParse(args[2], out c))
            {

                double squarePart = b * b - 4 * a * c;
                if (squarePart < 0)
                {
                    Console.WriteLine("Polynom have no solution");
                }
                else if (squarePart == 0)
                {
                    double root = -b / (2 * a);
                    Console.WriteLine("Solution is {0}", root);
                }
                else
                {
                    double root1 = -(b - Math.Sqrt(squarePart)) / (2 * a);
                    double root2 = -(b + Math.Sqrt(squarePart)) / (2 * a);
                    Console.WriteLine("Solutions are {0}, {1}", root1, root2);
                }
            }
            else
            {
                Console.WriteLine("Wrong arguments");
            }
        }
    }
}
