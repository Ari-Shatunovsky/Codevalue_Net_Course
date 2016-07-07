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
            const string fileName = "..//..//Personnel.txt";
            FileToListReader fileToListReader = new FileToListReader();;
            try
            {
                List<string> data = fileToListReader.ReadFile(fileName);

                foreach (var text in data)
                {
                    Console.WriteLine(text);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (IOException)
            {
                Console.WriteLine("IO exception");
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
