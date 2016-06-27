using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeLib;

namespace ShapesApp
{
    public class ShapeManager
    {

        public ShapeManager()
        {
            shapesList = new ArrayList();
        }

        private ArrayList shapesList;

        public void Add(Shape shape)
        {
            shapesList.Add(shape);
        }

        public void DisplayAll()
        {
            foreach (Shape shape in shapesList)
            {
                shape.Display();
                Console.WriteLine("Area is : {0}", shape.Area);
            }
        }

        public Shape this[int index]
        {
            get { return (Shape) shapesList[index]; }
        }

        public int Count
        {
            get { return shapesList.Count; }
        }

        public void Save(StringBuilder sb)
        {
            foreach (IPersist persist in shapesList)
            {
                persist.Write(sb);
            }
        }

        public void Sort()
        {
            shapesList.Sort();
        }
    }
}
