using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    public class FileToListReader
    {
        public List<string> ReadFile(string fileName)
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
