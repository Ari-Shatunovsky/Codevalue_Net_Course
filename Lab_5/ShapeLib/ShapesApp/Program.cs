using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeLib;

namespace ShapesApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ShapeManager shapeManager = new ShapeManager();
            shapeManager.Add(new Circle(4));

            Rectangle rectangle = new Rectangle(6, 8);
            rectangle.Color = ConsoleColor.DarkGreen;
            shapeManager.Add(rectangle);
            shapeManager.Add(new Ellipse(5, 10));
            shapeManager.Add(new Circle(12));

            shapeManager.DisplayAll();
            Console.WriteLine("Shapes count: {0}", shapeManager.Count);

            StringBuilder sb = new StringBuilder();

            shapeManager.Save(sb);
            Console.WriteLine(sb.ToString());

            ShapeManager rectangleManager = new ShapeManager();
            rectangleManager.Add(new Rectangle(4, 5));
            rectangleManager.Add(new Rectangle(1, 2));
            rectangleManager.Add(new Rectangle(5, 3));
            rectangleManager.Add(new Rectangle(8, 7));
            rectangleManager.Sort();
            rectangleManager.DisplayAll();
        }
    }
}