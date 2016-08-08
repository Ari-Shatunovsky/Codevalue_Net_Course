using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObj
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqHelper linqHelper = new LinqHelper();
            linqHelper.PrintMsCoreLibStuff();
            linqHelper.PrintProcessesGrouped();
            linqHelper.PrintTotalThreads();
            linqHelper.PrintProcesses();

            var a = new A()
            {
                PropertyA = 5,
                PropertyB = "Hey",
                PropertyD = "Hoi"
            };

            var b = new B();

            Console.WriteLine($"A: {a.ToString()}");
            Console.WriteLine($"B before copy: {b.ToString()}");
            a.CopyTo(b);
            Console.WriteLine($"B after copy: {b.ToString()}");
            Console.ReadLine();
        }


    }
}
