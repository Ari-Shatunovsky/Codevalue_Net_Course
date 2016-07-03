using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "C:\\Projects\\Learn\\Codevalue_Net_Course\\Lab_10\\Personnel\\Personnel.txt";
            FileStream fileStream = new FileStream(fileName, FileMode.Open,
                FileAccess.Read, FileShare.None);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, (int) fileStream.Length);
            string[] text = Encoding.UTF8.GetString(data).Split('\n');

            foreach (var person in text)
            {
                Console.WriteLine(person);
            }
            
            Console.ReadLine();
        }
    }
}
