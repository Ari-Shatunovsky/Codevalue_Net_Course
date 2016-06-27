using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primes
{
    class Program
    {
        static void Main(string[] args)
        {
            int start;
            int end;

            Console.WriteLine("Enter primes start:");

            string result = Console.ReadLine();

            while (!Int32.TryParse(result, out start))
            {
                Console.WriteLine("Not a valid number, try again.");
            }

            Console.WriteLine("Enter primes end:");

            result = Console.ReadLine();

            while (!Int32.TryParse(result, out end))
            {
                Console.WriteLine("Not a valid number, try again.");
            }

            int[] primes = CalcPrimes(start, end);

            foreach (var prime in primes)
            {
                Console.WriteLine(prime);
            }
        }

        static int[] CalcPrimes(int start, int end)
        {
            
            ArrayList totalPrimes = new ArrayList();
            ArrayList rangedPrimes = new ArrayList();
            totalPrimes.Add(2);
            if (start <= 2)
            {
                rangedPrimes.Add(2);
            }

            for (int i = 3; i < end; i += 2)
            {
                var isPrime = IsPrime(totalPrimes, i);
                if (isPrime)
                {
                    totalPrimes.Add(i);
                    if (start <= i)
                    {
                        rangedPrimes.Add(i);
                    }
                }
            }
            int[] result = new int[rangedPrimes.Count];
            rangedPrimes.CopyTo(result);
            return result;
        }

        private static bool IsPrime(ArrayList totalPrimes, int i)
        {
            bool isPrime = true;
            foreach (int prime in totalPrimes)
            {
                if (prime > (int) Math.Floor(Math.Sqrt(i)))
                {
                    break;
                }
                if (i%prime == 0)
                {
                    isPrime = false;
                }
            }
            return isPrime;
        }
    }
}
