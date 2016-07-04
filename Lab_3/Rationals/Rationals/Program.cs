using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rationals
{
    class Program
    {
        static void Main(string[] args)
        {
            Rational r1 = new Rational(48, 64);
            Rational r2 = new Rational(72, 48);
            r1.Reduce();
            Rational r3 = r1 + r2;
            Rational r4 = new Rational(6, 8);
            Rational r5 = r4 - r1;
            Rational r6 = r4 / r1;
            Console.WriteLine(r1.ToString());
            Console.WriteLine(r3.ToString());
            Console.WriteLine(r5.ToString());
            Console.WriteLine(r6.ToString());
            Console.WriteLine(r4.Equals(r1).ToString());
        }
    }
}
