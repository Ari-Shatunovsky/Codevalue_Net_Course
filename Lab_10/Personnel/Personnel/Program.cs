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
            string fileName = "..//..//Personnel.txt";

            try
            {
                List<string> data = ReadFile(fileName);

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

        static List<string> ReadFile(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader reader = new StreamReader(fileStream);

            List<string> data = new List<string>();

            while (!reader.EndOfStream)
            {
                data.Add(reader.ReadLine());
            }

            return data;
        }
    }
}
