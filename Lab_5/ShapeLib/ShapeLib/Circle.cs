using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Circle : Ellipse
    {

        public Circle(double radius) : base(radius, radius)
        {
            
        }

        public override void Display()
        {
            Console.WriteLine("Circle r = {0}", Radius1);
        }
    }
}
