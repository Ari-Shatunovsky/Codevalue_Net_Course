using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IMultiDictionary<int, string> multiDictionary = new MultiDictionary<int, string>();
            multiDictionary.Add(1, "one");
            multiDictionary.Add(1, "ich");
            multiDictionary.Add(1, "odin");
            multiDictionary.Add(2, "two");
            multiDictionary.Add(2, "nee");
            multiDictionary.Add(2, "dva");
            multiDictionary.Add(3, "three");
            multiDictionary.Add(3, "sun");
            multiDictionary.Add(3, "tri");

            Console.WriteLine("Is contains key 1?");
            Console.WriteLine(multiDictionary.ContainsKey(1));

            Console.WriteLine("Is contains key 5?");
            Console.WriteLine(multiDictionary.ContainsKey(5));

            Console.WriteLine("Is contains pair [1, odin]?");
            Console.WriteLine(multiDictionary.Contains(1, "odin"));

            Console.WriteLine("Is contains pair [2, shtain]?");
            Console.WriteLine(multiDictionary.Contains(2, "shtain"));

            foreach (var pair in multiDictionary)
            {
                Console.WriteLine(pair);
            }

            Console.WriteLine("Count is: {0}", multiDictionary.Count);

            Console.WriteLine("Keys:");
            foreach (var key in multiDictionary.Keys)
            {
                Console.WriteLine(key);
            }

            Console.WriteLine("Values:");
            foreach (var key in multiDictionary.Values)
            {
                Console.WriteLine(key);
            }

            Console.WriteLine("Is remove key 6 successfull? {0}", multiDictionary.Remove(6));
            Console.WriteLine("Is remove pair [2, two] successfull? {0}", multiDictionary.Remove(2, "two"));

            foreach (var pair in multiDictionary)
            {
                Console.WriteLine(pair);
            }
        }
    }
}
