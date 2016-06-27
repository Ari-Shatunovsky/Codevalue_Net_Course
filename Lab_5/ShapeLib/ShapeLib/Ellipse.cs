using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Ellipse : Shape, IPersist, IComparable<Ellipse>
    {
        public double Radius1 { get; set; }
        public double Radius2 { get; set; }

        public Ellipse(double radius1, double radius2)
        {
            Radius1 = radius1;
            Radius2 = radius2;
        }

        public override double Area
        {
            get { return Radius1 * Radius2 * Math.PI; }
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Ellipse r1 = {0}, r2 = {1}", Radius1, Radius2);
        }

        public void Write(StringBuilder sb)
        {
            sb.AppendLine($"r1 = {Radius1}, r2 = {Radius2}");
        }

        public int CompareTo(Ellipse other)
        {
            return (Area.CompareTo(other.Area));
        }
    }
}
