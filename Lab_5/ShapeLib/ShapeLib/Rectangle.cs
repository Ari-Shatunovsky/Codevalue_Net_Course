using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Rectangle : Shape, IPersist, IComparable<Rectangle>
    {
        double Width { get; set; }
        double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double Area
        {
            get { return Width * Height; } 
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Rectangle {0} x {1}", Width, Height);
        }

        public void Write(StringBuilder sb)
        {
            sb.AppendLine($"w = {Width}, h = {Height}");
        }

        public int CompareTo(Rectangle other)
        {
            Shape shape = other as Shape;
            if (shape != null)
            {
                return (int)(Area - shape.Area);
            }
                return -1;
        }
    }
} 
