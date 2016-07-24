using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShehBeshLib;

namespace ConsoleSheshBesh
{
    class Program
    {
        static void Main(string[] args)
        {
            GameProcessor gameProcessor = new GameProcessor();
            gameProcessor.CreateNewGame();
            gameProcessor.StartGame();
        }
    }
}
