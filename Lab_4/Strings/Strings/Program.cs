using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {

            string text = " ";

            while (!string.IsNullOrEmpty(text))
            {

                text = Console.ReadLine();

                string[] words = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                
                Console.WriteLine("Number of words is {0}", words.Length);

                Console.WriteLine("Original array:");
                DisplayArray(words);

                Array.Reverse(words);

                Console.WriteLine("Reversed array:");
                DisplayArray(words);

                Array.Sort(words);

                Console.WriteLine("Sorted array:");
                DisplayArray(words);

            }
        }

        static void DisplayArray(string[] words)
        {
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("");
        }
    }
}
