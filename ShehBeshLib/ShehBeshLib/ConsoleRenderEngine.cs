using System;

namespace ShehBeshLib
{
    public class ConsoleRenderEngine : IRenderEngine
    {
        public void RenderFiled(GameField field)
        {
            Console.WriteLine("SomeField");
        }

        private string BuildRow(GameField field)
        {
            return "";
        }
    }
}